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

  // bookings: Array<BookingModel> = [
  //   {
  //     date: "",
  //     userFullName: "Alissa White-Gluz",
  //     bookingType: 1,
  //     tableNumber: 1,
  //   },
  //   {
  //     date: "",
  //     userFullName: "Kirk Hammet",
  //     bookingType: 1,
  //     tableNumber: 1,
  //   },
  //   {
  //     date: "",
  //     userFullName: "Phil Anselmo",
  //     bookingType: 2,
  //     tableNumber: 1,
  //   },
  //   {
  //     date: "",
  //     userFullName: "Johannes Eckerström",
  //     bookingType: 3,
  //     tableNumber: 1,
  //   },
  //   {
  //     date: "",
  //     userFullName: "Tomas Haake",
  //     bookingType: 2,
  //     tableNumber: 1,
  //   },
  //   {
  //     date: "",
  //     userFullName: "Mario Duplantier",
  //     bookingType: 1,
  //     tableNumber: 1,
  //   },
  //   {
  //     date: "",
  //     userFullName: "Serj Tankian",
  //     bookingType: 1,
  //     tableNumber: 1,
  //   },
  //   {
  //     date: "",
  //     userFullName: "Dave Grohl",
  //     bookingType: 1,
  //     tableNumber: 1,
  //   },
  // ];
  
  bookings: BookingModel[] = [];

  constructor(private readonly bookingService: BookingService, private readonly dateService: DateService) { }

  ngOnInit(): void {
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
    
        if (x[0].booking.date != null) {
          this.bookings.sort(this.compare).reverse();
        } else {
          this.bookings[0].date = 'Permanent';
        }
    });
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
