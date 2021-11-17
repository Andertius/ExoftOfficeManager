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
      .get<Array<BookingResponse>>('https://localhost:44377/Booking/users/D5F659CB-04E2-4550-4896-08D99ACF32E4/bookings')
      ;

        console.log(result);
    //return result;
  }
}
