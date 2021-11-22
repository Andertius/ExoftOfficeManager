import { AfterViewInit, Component, ElementRef, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BookingService } from 'src/app/core/services/booking.service';
import { DateService } from 'src/app/core/services/date.service';
import { WorkPlaceService } from 'src/app/core/services/work-place.service';
import { BookingModel } from 'src/app/models/booking.model';
import { BookingType } from 'src/app/models/enums/booking-type.enum';
import { WorkPlaceResponse } from 'src/app/models/responses/work-placeRespnose.model';
import { ProfileComponent } from 'src/app/shared/components/profile/profile.component';
import { BookPlaceComponent } from '../book-place/book-place.component';

@Component({
  selector: 'app-tables',
  templateUrl: './tables.component.html',
  styleUrls: ['./tables.component.scss'],
  host: { class: 'app-tables' }
})
export class TablesComponent implements OnInit, AfterViewInit {

  clusterRange = [...Array(10).keys()];
  tableRange = [...Array(3).keys()];
  tables: Array<{
    tableNumber: number,
    bookingType: BookingType,
    bookingType2: BookingType,
  }> = [];

  bookings: BookingModel[] = [];
  date: string = "";
  
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
    this.date = this.dateService.prettyDate(new Date()).toUpperCase();
    this.bookingService.getSpecificDayBookings(new Date('2021-10-10'))
      .subscribe(x => {
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
      });
  }

  ngAfterViewInit() {
    const dom: HTMLElement = this.elementRef.nativeElement;
    const freePlaces = dom.querySelectorAll('.free-place');

    for (var i = 0; i < freePlaces.length; i++) {
      freePlaces[i].addEventListener('click', this._bookTable, false);
    }
    
    const bookedPlaces = dom.querySelectorAll('.booked-place');
    const bookedPermanentlyPlaces = dom.querySelectorAll('.booked-permanently-place');
    const halfBookedPlaces = dom.querySelectorAll('.half-booked-place');

    for (var i = 0; i < bookedPlaces.length; i++) {
      bookedPlaces[i].addEventListener('click', this._peekAtProfile, false);
    }

    for (var i = 0; i < bookedPermanentlyPlaces.length; i++) {
      bookedPermanentlyPlaces[i].addEventListener('click', this._peekAtProfile, false);
    }

    for (var i = 0; i < halfBookedPlaces.length; i++) {
      halfBookedPlaces[i].addEventListener('click', this._peekAtProfile, false);
    }
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
            workPlaceId: workPlace.workPlace.id,
            userId: "1D0BEA4F-DD83-4F45-F647-08D9ADBE1ABA",
            bookingType: 0,
            bookingDate: new Date(),
            days: null,
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
    const booking = this.bookings.slice().reverse().find(x => x.tableNumber === tableNumber);
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
