import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BookingModel } from 'src/app/models/booking.model';
import { BookingResponse } from 'src/app/models/responses/booking.response';
import { DateService } from './date.service';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  constructor(private readonly http: HttpClient, private readonly dateService: DateService) { }

  public getUserBookings(): Observable<BookingResponse[]> {
    return this.http.get<Array<BookingResponse>>(
        'https://localhost:44377/Booking/users/4D7BE074-A7B0-4536-719E-08D9ACF1D8DD/bookings');
  }

  public getSpecificDayBookings(date: Date): Observable<BookingResponse[]> {
    return this.http.get<Array<BookingResponse>>(
        `https://localhost:44377/Booking/bookings?bookingDate=${date.toISOString().split('T')[0]}`);
  }

  public subscribe(serverResponse: Observable<Array<BookingResponse>>): BookingModel[] {
    let bookings: BookingModel[] = [];

    serverResponse
      .subscribe(x => {
        for (let i = 0; i < x.length; i++) {
          bookings.push({
            userFullName: x[i].booking.user.fullName,
            date: this.dateService.prettyDate(x[i].booking.date),
            bookingType: x[i].booking.type,
            tableNumber: x[i].booking.workPlace.placeNumber,
            floorNumber: x[i].booking.workPlace.floorNumber,
          });
        }
        
        bookings = bookings.filter(x => x.floorNumber === 5);

        if (x[0].booking.date != null) {
          bookings.sort(this.compare).reverse();
        } else {
          bookings[0].date = 'Permanent';
        }
      });

    return bookings;
  }

  compare(a: BookingModel, b: BookingModel): number {
    if (a.date < b.date) {
      return -1;
    } else if (a.date > b.date) {
      return 1;
    }

    return 0;
  }

}
