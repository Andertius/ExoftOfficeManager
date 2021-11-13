import { Component, OnInit } from '@angular/core';
import { BookingModel } from 'src/app/models/booking.model';

@Component({
  selector: 'app-your-bookings',
  templateUrl: './your-bookings.component.html',
  styleUrls: ['./your-bookings.component.scss']
})
export class YourBookingsComponent implements OnInit {

  bookings: Array<BookingModel> = [
    {
      date: new Date(),
      userFullName: "Alissa White-Gluz",
      bookingType: 1
    },
    {
      date: new Date(),
      userFullName: "Alissa White-Gluz",
      bookingType: 1
    },
    {
      date: new Date(),
      userFullName: "Alissa White-Gluz",
      bookingType: 2
    },
    {
      date: new Date(),
      userFullName: "Alissa White-Gluz",
      bookingType: 3
    },
    {
      date: new Date(),
      userFullName: "Alissa White-Gluz",
      bookingType: 2
    },
    {
      date: new Date(),
      userFullName: "Alissa White-Gluz",
      bookingType: 1
    },
  ];

  constructor() { }

  ngOnInit(): void {
    this.bookings[0].date.setDate(17);
    this.bookings[0].date.setMonth(9);
    this.bookings[0].date.setFullYear(2069);
    
    this.bookings[1].date.setDate(18);
    this.bookings[1].date.setMonth(9);
    this.bookings[1].date.setFullYear(2069);
    
    this.bookings[2].date.setDate(19);
    this.bookings[2].date.setMonth(9);
    this.bookings[2].date.setFullYear(2069);
    
    this.bookings[3].date.setDate(20);
    this.bookings[3].date.setMonth(9);
    this.bookings[3].date.setFullYear(2069);
    
    this.bookings[4].date.setDate(21);
    this.bookings[4].date.setMonth(9);
    this.bookings[4].date.setFullYear(2069);
    
    this.bookings[5].date.setDate(22);
    this.bookings[5].date.setMonth(9);
    this.bookings[5].date.setFullYear(2069);

    this.bookings.sort(this.compare).reverse();
  }

  compare(a : BookingModel, b: BookingModel): number {
    if (a.date < b.date) {
      return -1;
    } else if (a.date > b.date) {
      return 1;
    }
    
    return 0;
  }

  parseMonth(month: number): string {
    let result: string = "";

    switch (month) {
      case 0:
        result = "Jan";
        break;

      case 1:
        result = "Feb";
        break;

      case 2:
        result = "Mar";
        break;

      case 3:
        result = "Apr";
        break;

      case 4:
        result = "May";
        break;

      case 5:
        result = "Jun";
        break;

      case 6:
        result = "Jul";
        break;
        
      case 7:
        result = "Aug";
        break;
        
      case 8:
        result = "Sep";
        break;
        
      case 9:
        result = "Oct";
        break;
        
      case 10:
        result = "Nov";
        break;
        
      case 11:
        result = "Dec";
        break;

      default:
        console.log('Invalid month');
    }

    return result;
  }

}
