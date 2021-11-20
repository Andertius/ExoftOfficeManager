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

  range: number[] = Array(10);
  bookings: BookingModel[] = [];
  date: string = "";

  constructor(private readonly dateService: DateService, private readonly bookingService: BookingService) { }

  ngOnInit(): void {
    this.date = this.dateService.prettyDate(new Date()).toUpperCase();
    this.bookings = this.bookingService.getUserBookings();
    this.bookings.find(x => x.tableNumber == 1)
  }

}
