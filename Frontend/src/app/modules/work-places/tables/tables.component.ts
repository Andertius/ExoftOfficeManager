import { Component, ElementRef, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { BookingService } from 'src/app/core/services/booking.service';
import { DateService } from 'src/app/core/services/date.service';
import { WorkPlaceService } from 'src/app/core/services/work-place.service';
import { BookingModel } from 'src/app/models/booking.model';
import { BookingType } from 'src/app/models/enums/booking-type.enum';
import { DatePickerComponent } from 'src/app/shared/components/date-picker/date-picker.component';
import { ProfileComponent } from 'src/app/shared/components/profile/profile.component';
import { BookPlaceComponent } from '../book-place/book-place.component';

@Component({
  selector: 'app-tables',
  templateUrl: './tables.component.html',
  styleUrls: ['./tables.component.scss'],
  host: { class: 'app-tables' }
})
export class TablesComponent implements OnInit {
  
  private unsubscribe$: Subject<void> = new Subject();

  clusterRange = [...Array(10).keys()];
  tableRange = [...Array(3).keys()];
  tables: Array<{
    tableNumber: number,
    bookingType: BookingType,
    bookingType2: BookingType,
  }> = [];

  bookings: BookingModel[] = [];
  date: Date = new Date();
  dateString: string = "";
  
  _bookTable = this.bookTable.bind(this);
  _peekAtProfile = this.peekAtProfile.bind(this);

  constructor(
    private readonly dateService: DateService,
    private readonly bookingService: BookingService,
    private readonly workPlaceService: WorkPlaceService,
    private readonly elementRef: ElementRef,
    private readonly dialog: MatDialog) { }

  public get bookingType(): typeof BookingType {
    return BookingType;
  }

  ngOnInit(): void {
    this.dateString = this.dateService.prettyDate(this.date).toUpperCase();
    this.getBookings();

    this.dateService.behaviourSubject
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(x => {
        this.date = x;
        this.dateString = this.dateService.prettyDate(this.date).toUpperCase();
        this.bookingService.getSpecificDayBookings(this.date).subscribe();
      }
    );

    this.bookingService.behaviourSubject
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(x => {
        if (this.dateService.prettyDate(new Date(x.date)).toUpperCase() === this.dateString) {
          this.bookings.push({
            userFullName: x.userFullName,
            date: this.dateService.prettyDate(new Date(x.date)),
            bookingType: x.bookingType,
            tableNumber: x.tableNumber,
            floorNumber: x.floorNumber,
          });

        debugger;
          let index = this.tables.findIndex(table => table.tableNumber == x.tableNumber);
          let table = this.tables[index];
          let button: HTMLElement | null;
  
          if (table.bookingType === BookingType.Available) {
            this.tables[index].bookingType = x.bookingType;
            button = document.getElementById(`${x.tableNumber}.0`);
  
            let button2 = document.getElementById(`${x.tableNumber}.5`);
  
            button2?.addEventListener('click', this._bookTable);
          } else {
            this.tables[index].bookingType2 = x.bookingType;
            button = document.getElementById(`${x.tableNumber}.5`);
          }
  
          button?.removeEventListener('click', this._bookTable);
          button?.addEventListener('click', this._peekAtProfile);
        }
      });

    this.bookingService.behaviourSubject1
    .pipe(takeUntil(this.unsubscribe$))
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
  }

  getBookings(): void {
    this.bookingService.getSpecificDayBookings(this.date)
      .subscribe(x => {
        this.bookings = [];
        this.tables = [];

        for (let i = 0; i < x.length; i++) {
          this.bookings.push({
            userFullName: x[i].booking.user.fullName,
            date: this.dateService.prettyDate(x[i].booking.date),
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
      }
    );
  }

  openDatePicker(event: Event): void {
    
    const dialogRef = this.dialog.open(DatePickerComponent, {
      data: {
        date: new Date(),
        trigger: new ElementRef(event.currentTarget),
      }
    });

    dialogRef.afterClosed()
      .subscribe((x: Date) => {
        if (x !== undefined) {
          this.dateService.behaviourSubject = x;
        }
      }
    );
  }

  peekAtProfile(event: Event): void {
    const button = event.currentTarget as HTMLButtonElement;
    const target = new ElementRef(event.currentTarget);

    const table = Number(button.id.slice(0, button.id.length - 2));

    const booking = button.id[button.id.length - 1] === "0" ?
      this.bookings.filter(x => x.tableNumber === table)[0] : this.bookings.filter(x => x.tableNumber === table)[1];

    const dialogRef = this.dialog.open(ProfileComponent, {
      data: {
        username: booking.userFullName,
        workPlace: { tableNumber: table, floorNumber: 5 },
        date: booking.date,
        bookingType: booking.bookingType,
        trigger: target,
      }
    });
  }

  bookTable(event: Event) {
    const button = event.currentTarget as HTMLButtonElement;

    this.workPlaceService.findWorkPlaceByPlaceNumber(Number(button.id.slice(0, button.id.length - 2)), 5)
      .subscribe(workPlace => {
        const dialogRef = this.dialog.open(BookPlaceComponent, {
          data: {
            request: {
              workPlaceId: workPlace.workPlace.id,
              userId: "D5219B5E-E72E-4E3A-49C5-08D9AF3D83EB",
              bookingType: 0,
              bookingDate: new Date(),
              days: null,
            },
            userFullName: "Alissa White-Gluz",
            tableNumber: Number(button.id.slice(0, button.id.length - 2)),
            floorNumber: 5,
          },
        });
      }
    );    
  }

  getBookingType(tableNumber: number): BookingType {
    const booking = this.bookings.find(x => x.tableNumber === tableNumber);
    return booking?.bookingType ?? BookingType.Available;
  }

  getBookingType2(tableNumber: number): BookingType {
    let booking = this.bookings.slice().reverse().find(x => x.tableNumber === tableNumber);

    if (booking?.bookingType === this.getBookingType(tableNumber)) {
      return BookingType.Available;
    }

    return booking?.bookingType ?? BookingType.Available;
  }

  getBookingName(tableNumber: number): string {
    const booking = this.bookings.find(x => x.tableNumber === tableNumber);
    return booking?.userFullName ?? "";
  }

  getBookingName2(tableNumber: number): string {
    const booking = this.bookings.slice().reverse().find(x => x.tableNumber === tableNumber);
    return booking?.userFullName ?? "";
  }

}
