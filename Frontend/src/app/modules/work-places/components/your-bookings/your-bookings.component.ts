import { BookingService } from 'src/app/shared/services/booking.service';
import { ErrorService } from 'src/app/shared/services/error.service';
import { ProfileService } from 'src/app/core/services/profile.service';

import { Booking } from 'src/app/models/booking.model';
import { BookingType } from 'src/app/models/enums/booking-type.enum';
import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { EditProfileResult } from 'src/app/models/edit-profile-result.model';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
    selector: 'app-your-bookings',
    templateUrl: './your-bookings.component.html',
    styleUrls: ['./your-bookings.component.scss']
})
export class YourBookingsComponent implements OnInit, OnDestroy {
    @Input() id!: string;

    private _unsubscribe$: Subject<void> = new Subject();

    public bookings: Array<Booking> = new Array<Booking>();

    constructor(
        private readonly _bookingService: BookingService,
        private readonly _profileService: ProfileService,
        private readonly _errorService: ErrorService)
    { }

    public get bookingType(): typeof BookingType {
        return BookingType;
    }

    public ngOnInit(): void {
        this.bookings = this._bookingService
            .bookingResponseObservableToModel(this._bookingService
                .getUserBookings(this.id));

        this._profileService.profileSubject
            .pipe(takeUntil(this._unsubscribe$))
            .subscribe((res: EditProfileResult) => {
                this.bookings.forEach(x => x.userFullName = res.fullName);
            });
        
        this._errorService.errorSubject
            .pipe(takeUntil(this._unsubscribe$))
            .subscribe(x => console.log(x));
    }

    public ngOnDestroy(): void {
        this._unsubscribe$.next();
        this._unsubscribe$.complete();
    }
}
