import { DateService } from './date.service';
import { ErrorService } from './error.service';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, Subject } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Booking } from 'src/app/models/booking.model';
import { AddBookingRequest } from 'src/app/models/requests/add-booking-request.model';
import { BookingResponse } from 'src/app/models/responses/booking-response.model';

@Injectable({
    providedIn: 'root'
})
export class BookingService {
    private _userBookingsSubject$: Subject<Booking> = new Subject();
    private _specificDayBookingsSubject$: Subject<Booking[]> = new Subject();

    public get userBookingsSubject(): Subject<Booking> {
        return this._userBookingsSubject$;
    }

    public setUserBookingsSubject(value: Booking) {
        this._userBookingsSubject$.next(value);
    }

    public get specificDayBookingsSubject(): Subject<Booking[]> {
        return this._specificDayBookingsSubject$;
    }

    public setSpecificDayBookingsSubject(value: Booking[]) {
        this._specificDayBookingsSubject$.next(value);
    }

    constructor(
        private readonly _http: HttpClient,
        private readonly _dateService: DateService,
        private readonly _errorService: ErrorService)
    { }

    public getUserBookings(): Observable<BookingResponse[]> {
        return this._http.get<Array<BookingResponse>>(
            'https://localhost:44377/Booking/users/1D0BEA4F-DD83-4F45-F647-08D9ADBE1ABA/bookings')
            .pipe(catchError(err => {
                if (err.status === 400) {
                    this._errorService.setErrorSubject(err.error.message);
                }

                return of([]);
            }));
    }

    public getSpecificDayBookings(date: Date): Observable<BookingResponse[]> {
        return this._http.get<Array<BookingResponse>>(
            `https://localhost:44377/Booking/bookings?bookingDate=${date.toISOString().split('T')[0]}`)
            .pipe(
                tap(res => {
                    this.specificDayBookingsSubject.next(this.bookingResponsesToModels(res))
                }),
                catchError(err => {
                    if (err.status === 400) {
                        this._errorService.setErrorSubject(err.error.message);
                    }

                    return of([]);
                }));
    }

    public bookingResponseObservableToModel(serverResponse: Observable<Array<BookingResponse>>): Booking[] {
        let bookings: Booking[] = [];

        serverResponse
            .subscribe(x => {
                for (let i = 0; i < x.length; i++) {
                    bookings.push({
                        userFullName: x[i].booking.user.fullName,
                        date: this._dateService.prettyDate(x[i].booking.date),
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

    public bookingResponsesToModels(response: BookingResponse[]): Booking[] {
        let bookings: Booking[] = [];

        if (response.length !== 0) {
            for (let i = 0; i < response.length; i++) {
                bookings.push({
                    userFullName: response[i].booking.user.fullName,
                    date: this._dateService.prettyDate(response[i].booking.date),
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
        this._http.post(`https://localhost:44377/WorkPlace/work-places/${data.workPlaceId}/book`, {
            userId: data.userId,
            bookingType: data.bookingType,
            bookingDate: data.bookingDate,
            days: data.days
        },
        {
            observe: 'response'
        })
            .pipe(catchError(err => {
                if (err.status === 400) {
                    this._errorService.setErrorSubject(err.error.message);
                }

                return of(0);
            }))
            .subscribe();
    }

    private compare(a: Booking, b: Booking): number {
        if (a.date < b.date) {
            return -1;
        } else if (a.date > b.date) {
            return 1;
        }

        return 0;
    }
}
