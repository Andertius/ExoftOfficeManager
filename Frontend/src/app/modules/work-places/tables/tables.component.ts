import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { BookingService } from 'src/app/core/services/booking.service';
import { DateService } from 'src/app/core/services/date.service';
import { BookingModel } from 'src/app/models/booking.model';

@Component({
  selector: 'app-tables',
  templateUrl: './tables.component.html',
  styleUrls: ['./tables.component.scss'],
  host: { class: 'app-tables' }
})
export class TablesComponent implements OnInit, AfterViewInit {

  range = [...Array(10).keys()];
  tables: Array<{
    tableNumber: number,
    bookingType: number,
    bookingType2: number,
  }> = [];

  bookings: BookingModel[] = [];
  date: string = "";
  
  _bookTable = this.bookTable.bind(this);

  constructor(
    private readonly dateService: DateService,
    private readonly bookingService: BookingService,
    private readonly elementRef: ElementRef) { }

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
    const elements = dom.querySelectorAll('.free-place');

    for (var i = 0; i < elements.length; i++) {
      elements[i].addEventListener('click', this._bookTable, false);
    }
  } 

  bookTable(event: Event) {
    console.log('a');
  }

  getBookingType(tableNumber: number): number {
    const booking = this.bookings.find(x => x.tableNumber === tableNumber);
    return booking?.bookingType ?? 0;
  }

  getBookingType2(tableNumber: number): number {
    const booking = this.bookings.slice().reverse().find(x => x.tableNumber === tableNumber);
    return booking?.bookingType ?? 0;
  }

  getBookingName(tableNumber: number): string {
    const booking = this.bookings.find(x => x.tableNumber === tableNumber);
    return booking?.userFullName ?? "";
  }

  getBookingName2(tableNumber: number): string {
    const booking = this.bookings.slice().reverse().find(x => x.tableNumber === tableNumber);
    return booking?.userFullName ?? "";
  }

  openProfile(event: Event): void {
    
  }

}
