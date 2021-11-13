import { Component, OnInit } from '@angular/core';
import { BookingModel } from 'src/app/models/booking.model';

@Component({
  selector: 'app-current-day-bookings',
  templateUrl: './current-day-bookings.component.html',
  styleUrls: ['./current-day-bookings.component.scss']
})
export class CurrentDayBookingsComponent implements OnInit {

  bookings: Array<BookingModel> = [
    {
      date: new Date(),
      userFullName: "Alissa White-Gluz",
      bookingType: 1
    },
    {
      date: new Date(),
      userFullName: "Kirk Hammet",
      bookingType: 1
    },
    {
      date: new Date(),
      userFullName: "Phil Anselmo",
      bookingType: 2
    },
    {
      date: new Date(),
      userFullName: "Johannes Eckerström",
      bookingType: 3
    },
    {
      date: new Date(),
      userFullName: "Tomas Haake",
      bookingType: 2
    },
    {
      date: new Date(),
      userFullName: "Mario Duplantier",
      bookingType: 1
    },
    {
      date: new Date(),
      userFullName: "Serj Tankian",
      bookingType: 1
    },
    {
      date: new Date(),
      userFullName: "Dave Grohl",
      bookingType: 1
    },
  ];
  
  constructor() { }

  ngOnInit(): void {
  }

}
