import { BookingService } from 'src/app/shared/services/booking.service';
import { DateService } from 'src/app/shared/services/date.service';
import { ErrorService } from 'src/app/shared/services/error.service';
import { ProfileService } from 'src/app/core/services/profile.service';

import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Booking } from 'src/app/models/booking.model';
import { EditProfileResult } from 'src/app/models/edit-profile-result.model';
import { BookingType } from 'src/app/models/enums/booking-type.enum';

@Component({
    selector: 'app-current-day-bookings',
    templateUrl: './current-day-bookings.component.html',
    styleUrls: ['./current-day-bookings.component.scss']
})
export class CurrentDayBookingsComponent implements OnInit, OnDestroy {
    private _unsubscribe$: Subject<void> = new Subject();

    public bookings: Booking[] = [];

    constructor(
        private readonly _profileService: ProfileService,
        private readonly _bookingService: BookingService,
        private readonly _dateService: DateService,
        private readonly _errorService: ErrorService) { }

    public get bookingType(): typeof BookingType {
        return BookingType;
    }

    public ngOnInit(): void {
        this._profileService.profileSubject
            .pipe(takeUntil(this._unsubscribe$))
            .subscribe((res: EditProfileResult) => {
                this.bookings.forEach(x => x.userFullName == res.prevName ? x.userFullName = res.fullName : '');
            });

        this._dateService.dateChangeSubject
            .pipe(takeUntil(this._unsubscribe$))
            .subscribe(res => {
                res.setHours(3);
                this._bookingService.getSpecificDayBookings(res).subscribe();
            })

        this._bookingService.specificDayBookingsSubject
            .pipe(takeUntil(this._unsubscribe$))
            .subscribe(res => {
                this.bookings = res;
            })

        this._bookingService.getSpecificDayBookings(new Date());

        this._errorService.errorSubject
            .pipe(takeUntil(this._unsubscribe$))
            .subscribe(x => alert(x));
    }

    public ngOnDestroy(): void {
        this._unsubscribe$.next();
        this._unsubscribe$.complete();
    }
}
