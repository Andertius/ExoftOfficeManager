import { Component, OnInit } from '@angular/core';
import { BookingService } from 'src/app/core/services/booking.service';
import { DateService } from 'src/app/core/services/date.service';
import { BookingModel } from 'src/app/models/booking.model';

@Component({
  selector: 'app-current-day-bookings',
  templateUrl: './current-day-bookings.component.html',
  styleUrls: ['./current-day-bookings.component.scss']
})
export class CurrentDayBookingsComponent implements OnInit {
  
  bookings: BookingModel[] = [];

  constructor(private readonly bookingService: BookingService) { }

  ngOnInit(): void {
    this.bookings = this.bookingService.getSpecificDayBookings(new Date('2021-10-10'));
  }

  compare(a : BookingModel, b: BookingModel): number {
    if (a.date < b.date) {
      return -1;
    } else if (a.date > b.date) {
      return 1;
    }
    
    return 0;
  }

}
