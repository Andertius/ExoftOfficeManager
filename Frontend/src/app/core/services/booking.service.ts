import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { BookingResponse } from 'src/app/models/responses/booking.response';
import { UserResponse } from 'src/app/models/responses/user.response';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  constructor(private readonly http: HttpClient) { }

  public getUserBookings(): Observable<BookingResponse[]> {
    let result: BookingResponse[] = [];

    return this.http
      .get<Array<BookingResponse>>('https://localhost:44377/Booking/users/3A0F30CE-B76B-496A-B7D7-08D99C4C73AC/bookings')
      ;

        console.log(result);
    //return result;
  }
}
