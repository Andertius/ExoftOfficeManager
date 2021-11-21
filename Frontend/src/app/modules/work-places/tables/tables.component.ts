import { Component, OnInit } from '@angular/core';
import { BookingService } from 'src/app/core/services/booking.service';
import { DateService } from 'src/app/core/services/date.service';
import { BookingModel } from 'src/app/models/booking.model';

@Component({
  selector: 'app-tables',
  templateUrl: './tables.component.html',
  styleUrls: ['./tables.component.scss'],
  host: { class: 'app-tables' }
})
export class TablesComponent implements OnInit {

  range = [...Array(10).keys()];
  tables: Array<{
    tableNumber: number,
    bookingType: number,
  }> = [];

  bookings: BookingModel[] = [];
  date: string = "";

  constructor(private readonly dateService: DateService, private readonly bookingService: BookingService) { }

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
          });
        }
        
        for (let i = 0; i < 30; i++) {
          this.tables.push({
            tableNumber: i + 1,
            bookingType: this.getBookingType(i + 1),
          });
        }
      });
  }

  getBookingType(tableNumber: number): number {
    const booking = this.bookings.find(x => x.tableNumber === tableNumber);
    return booking?.bookingType ?? 0;
  }

  getBookingName(tableNumber: number): string {
    const booking = this.bookings.find(x => x.tableNumber === tableNumber);
    return booking?.userFullName ?? "";
  }

}
