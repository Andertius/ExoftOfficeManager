import { X } from '@angular/cdk/keycodes';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';
import { BookingService } from 'src/app/core/services/booking.service';
import { DateService } from 'src/app/core/services/date.service';
import { ProfileService } from 'src/app/core/services/profile.service';
import { BookingModel } from 'src/app/models/booking.model';
import { EditProfileResultModel } from 'src/app/models/edit-profile-result.model';
import { BookingType } from 'src/app/models/enums/booking-type.enum';
import { BookingResponse } from 'src/app/models/responses/bookingResponse.model';

@Component({
  selector: 'app-your-bookings',
  templateUrl: './your-bookings.component.html',
  styleUrls: ['./your-bookings.component.scss']
})
export class YourBookingsComponent implements OnInit, OnDestroy {
  
  private unsubscribe$: Subject<void> = new Subject();

  bookings: Array<BookingModel> = new Array<BookingModel>();

  constructor(
    private readonly bookingService: BookingService,
    private readonly profileService: ProfileService) { }
    
  public get bookingType(): typeof BookingType {
    return BookingType;
  }

  ngOnInit(): void {
    this.bookings = this.bookingService.subscribe(this.bookingService.getUserBookings());
    
    this.profileService.behaviourSubject
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((res: EditProfileResultModel) => {
        this.bookings.forEach(x => x.userFullName = res.fullName);
      });
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

}
