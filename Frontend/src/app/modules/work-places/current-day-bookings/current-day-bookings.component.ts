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
      date: "",
      userFullName: "Alissa White-Gluz",
      bookingType: 1
    },
    {
      date: "",
      userFullName: "Kirk Hammet",
      bookingType: 1
    },
    {
      date: "",
      userFullName: "Phil Anselmo",
      bookingType: 2
    },
    {
      date: "",
      userFullName: "Johannes Eckerström",
      bookingType: 3
    },
    {
      date: "",
      userFullName: "Tomas Haake",
      bookingType: 2
    },
    {
      date: "",
      userFullName: "Mario Duplantier",
      bookingType: 1
    },
    {
      date: "",
      userFullName: "Serj Tankian",
      bookingType: 1
    },
    {
      date: "",
      userFullName: "Dave Grohl",
      bookingType: 1
    },
  ];
  
  constructor() { }

  ngOnInit(): void {
  }

}
