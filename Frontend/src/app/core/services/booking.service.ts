import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, Subject } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { BookingModel } from 'src/app/models/booking.model';
import { AddBookingRequest } from 'src/app/models/requests/addBookingRequest.model';
import { BookingResponse } from 'src/app/models/responses/bookingResponse.model';
import { DateService } from './date.service';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  
  private _behaviourSubject$: Subject<BookingModel> = new Subject();

  public get behaviourSubject(): Subject<BookingModel> {
      return this._behaviourSubject$;
  }

  public set behaviourSubject(value: any) {
      this._behaviourSubject$.next(value);
  }
  
  private _behaviourSubject1$: Subject<BookingModel[]> = new Subject();

  public get behaviourSubject1(): Subject<BookingModel[]> {
      return this._behaviourSubject1$;
  }

  public set behaviourSubject1(value: any) {
      this._behaviourSubject1$.next(value);
  }
  
  private _errorSubject$: Subject<string> = new Subject();

  public get errorSubject(): Subject<string> {
      return this._errorSubject$;
  }

  public set errorSubject(value: any) {
      this._errorSubject$.next(value);
  }

  constructor(private readonly http: HttpClient, private readonly dateService: DateService) { }

  public getUserBookings(): Observable<BookingResponse[]> {
    return this.http.get<Array<BookingResponse>>(
        'https://localhost:44377/Booking/users/1D0BEA4F-DD83-4F45-F647-08D9ADBE1ABA/bookings');
  }

  public getSpecificDayBookings(date: Date): Observable<BookingResponse[]> {
    return this.http.get<Array<BookingResponse>>(
        `https://localhost:44377/Booking/bookings?bookingDate=${date.toISOString().split('T')[0]}`).pipe(tap(res => {
          this.behaviourSubject1.next(this.bookingResponseToModel(res))
        }));
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

  bookingResponseToModel(response: BookingResponse[]): BookingModel[] {
    let bookings: BookingModel[] = [];

    if (response.length !== 0) {
      for (let i = 0; i < response.length; i++) {
        bookings.push({
          userFullName: response[i].booking.user.fullName,
          date: this.dateService.prettyDate(response[i].booking.date),
          bookingType: response[i].booking.type,
          tableNumber: response[i].booking.workPlace.placeNumber,
          floorNumber: response[i].booking.workPlace.floorNumber,
        });
      }
      
      bookings = bookings.filter(response => response.floorNumber === 5);
  
      if (response[0].booking.date != null) {
        bookings.sort(this.compare).reverse();
      } else {
        bookings[0].date = 'Permanent';
      }
    }

    return bookings;
  }

  public addBooking(data: AddBookingRequest): void {
    this.http.post(`https://localhost:44377/WorkPlace/work-places/${data.workPlaceId}/book`, {
      userId: data.userId,
      bookingType: data.bookingType,
      bookingDate: data.bookingDate,
      days: data.days
    }, { observe: 'response' })
    .pipe(catchError(err => {
      if (err.status === 400) {
        this.errorSubject = err.error.message;
      }

      return of(0);
    }))
    .subscribe();
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
