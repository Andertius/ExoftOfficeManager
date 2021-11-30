import { BookPlaceComponent } from '../book-place/book-place.component';
import { DatePickerComponent } from 'src/app/shared/components/date-picker/date-picker.component';
import { ProfileComponent } from 'src/app/shared/components/profile/profile.component';

import { BookingService } from 'src/app/shared/services/booking.service';
import { DateService } from 'src/app/shared/services/date.service';
import { ErrorService } from 'src/app/shared/services/error.service';
import { WorkPlaceService } from 'src/app/shared/services/work-place.service';

import { Component, ElementRef, Input, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Booking } from 'src/app/models/booking.model';
import { BookingType } from 'src/app/models/enums/booking-type.enum';

@Component({
    selector: 'app-tables',
    templateUrl: './tables.component.html',
    styleUrls: ['./tables.component.scss'],
    host: { class: 'app-tables' }
})
export class TablesComponent implements OnInit, OnDestroy {

    private _unsubscribe$: Subject<void> = new Subject();

    public clusterRange = [...Array(10).keys()];
    public tableRange = [...Array(3).keys()];
    public tables: Array<{
        tableNumber: number,
        bookingType: BookingType,
        bookingType2: BookingType,
    }> = [];

    public bookings: Booking[] = [];
    public date: Date = new Date();
    public dateString: string = "";

    constructor(
        private readonly _dateService: DateService,
        private readonly _bookingService: BookingService,
        private readonly _workPlaceService: WorkPlaceService,
        private readonly _errorService: ErrorService,
        private readonly _dialog: MatDialog) { }

    public get bookingType(): typeof BookingType {
        return BookingType;
    }

    public ngOnInit(): void {
        this.dateString = this._dateService.prettyDate(this.date).toUpperCase();
        this.getBookings();

        this._dateService.dateChangeSubject
            .pipe(takeUntil(this._unsubscribe$))
            .subscribe(x => {
                this.date = x;
                this.dateString = this._dateService.prettyDate(this.date).toUpperCase();
                this._bookingService.getSpecificDayBookings(this.date).subscribe();
            });

        this._bookingService.userBookingsSubject
            .pipe(takeUntil(this._unsubscribe$))
            .subscribe(x => {
                if (this._dateService.prettyDate(new Date(x.date)).toUpperCase() === this.dateString) {
                    this.bookings.push({
                        userFullName: x.userFullName,
                        date: this._dateService.prettyDate(new Date(x.date)),
                        bookingType: x.bookingType,
                        tableNumber: x.tableNumber,
                        floorNumber: x.floorNumber,
                    });

                    let index = this.tables.findIndex(table => table.tableNumber == x.tableNumber);
                    let table = this.tables[index];

                    if (table.bookingType === BookingType.Available) {
                        this.tables[index].bookingType = x.bookingType;
                    } else {
                        this.tables[index].bookingType2 = x.bookingType;
                    }
                }
            });

        this._bookingService.specificDayBookingsSubject
            .pipe(takeUntil(this._unsubscribe$))
            .subscribe(x => {
                this.bookings = [];
                this.tables = [];

                for (let i = 0; i < x.length; i++) {
                    this.bookings.push(x[i]);
                }

                for (let i = 0; i < 30; i++) {
                    this.tables.push({
                        tableNumber: i + 1,
                        bookingType: this.getBookingType(i + 1),
                        bookingType2: this.getBookingType2(i + 1),
                    });
                }
            });

        this._errorService.errorSubject
            .pipe(takeUntil(this._unsubscribe$))
            .subscribe(x => alert(x));
    }

    public ngOnDestroy(): void {
        this._unsubscribe$.next();
        this._unsubscribe$.complete();
    }

    public getBookings(): void {
        this._bookingService.getSpecificDayBookings(this.date)
            .subscribe(x => {
                this.bookings = [];
                this.tables = [];

                for (let i = 0; i < x.length; i++) {
                    this.bookings.push({
                        userFullName: x[i].booking.user.fullName,
                        date: this._dateService.prettyDate(x[i].booking.date),
                        bookingType: x[i].booking.type,
                        tableNumber: x[i].booking.workPlace.placeNumber,
                        floorNumber: x[i].booking.workPlace.floorNumber,
                    });
                }

                for (let i = 0; i < 30; i++) {
                    this.tables.push({
                        tableNumber: i + 1,
                        bookingType: this.getBookingType(i + 1),
                        bookingType2: this.getBookingType2(i + 1),
                    });
                }
            });
    }

    public openDatePicker(event: Event): void {
        const dialogRef = this._dialog.open(DatePickerComponent, {
            data: {
                date: new Date(),
                trigger: new ElementRef(event.currentTarget),
            }
        });

        dialogRef.afterClosed()
            .subscribe((x: Date) => {
                if (x !== undefined) {
                    this._dateService.setDateChangeSubject(x);
                }
            });
    }

    public peekAtProfile(event: Event): void {
        const button = event.currentTarget as HTMLButtonElement;
        const target = new ElementRef(event.currentTarget);

        const table = Number(button.id.slice(0, button.id.length - 2));

        const booking = button.id[button.id.length - 1] === "0" ?
            this.bookings.filter(x => x.tableNumber === table)[0] : this.bookings.filter(x => x.tableNumber === table)[1];

        this._dialog.open(ProfileComponent, {
            data: {
                username: booking.userFullName,
                workPlace: { tableNumber: table, floorNumber: 5 },
                date: booking.date,
                bookingType: booking.bookingType,
                avatar: "https://localhost:44377/images/avatars/image.jpg",
                trigger: target,
            }
        });
    }

    public bookTable(event: Event): void {
        const button = event.currentTarget as HTMLButtonElement;

        this._workPlaceService.findWorkPlaceByPlaceNumber(Number(button.id.slice(0, button.id.length - 2)), 5)
            .subscribe(workPlace => {
                this._dialog.open(BookPlaceComponent, {
                    data: {
                        request: {
                            workPlaceId: workPlace.workPlace.id,
                            userId: sessionStorage.getItem("sessionUserId") ?? "",
                            bookingType: 0,
                            bookingDate: new Date(),
                            days: null,
                        },
                        userFullName: sessionStorage.getItem("sessionUserFullName"),
                        tableNumber: Number(button.id.slice(0, button.id.length - 2)),
                        floorNumber: 5,
                        bookings: this.bookings,
                    },
                });
            });
    }

    public getBookingType(tableNumber: number): BookingType {
        const booking = this.bookings.find(x => x.tableNumber === tableNumber);
        return booking?.bookingType ?? BookingType.Available;
    }

    public getBookingType2(tableNumber: number): BookingType {
        let booking = this.bookings.slice().reverse().find(x => x.tableNumber === tableNumber);

        if (booking?.bookingType === this.getBookingType(tableNumber)) {
            return BookingType.Available;
        }

        return booking?.bookingType ?? BookingType.Available;
    }

    public getBookingName(tableNumber: number): string {
        const booking = this.bookings.find(x => x.tableNumber === tableNumber);
        return booking?.userFullName ?? "";
    }

    public getBookingName2(tableNumber: number): string {
        const booking = this.bookings.slice().reverse().find(x => x.tableNumber === tableNumber);
        return booking?.userFullName ?? "";
    }
}
