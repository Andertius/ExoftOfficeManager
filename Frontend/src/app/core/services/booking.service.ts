import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BookingResponse } from 'src/app/models/responses/booking.response';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  constructor(private readonly http: HttpClient) { }

  public getUserBookings(): Observable<BookingResponse[]> {
    return this.http.get<Array<BookingResponse>>('https://localhost:44377/Booking/users/3A0F30CE-B76B-496A-B7D7-08D99C4C73AC/bookings');
  }

  public getSpecificDayBookings(date: Date): Observable<BookingResponse[]> {
    return this.http.get<Array<BookingResponse>>(`https://localhost:44377/Booking/bookings?bookingDate=${date.toISOString().split('T')[0]}`);
  }
}
