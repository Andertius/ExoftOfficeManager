import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';
import { BookingService } from 'src/app/core/services/booking.service';
import { DateService } from 'src/app/core/services/date.service';
import { ProfileService } from 'src/app/core/services/profile.service';
import { BookingModel } from 'src/app/models/booking.model';
import { BookingResponse } from 'src/app/models/responses/booking.response';

@Component({
  selector: 'app-your-bookings',
  templateUrl: './your-bookings.component.html',
  styleUrls: ['./your-bookings.component.scss']
})
export class YourBookingsComponent implements OnInit, OnDestroy {
  
  private unsubscribe$: Subject<void> = new Subject();

  bookings: Array<BookingModel> = new Array<BookingModel>();
  bookingsResponses: BookingResponse[] = [];

  constructor(
    private readonly bookingService: BookingService,
    private readonly profileService: ProfileService,
    private readonly dateService: DateService) { }

  ngOnInit(): void {
    this.bookingService.getUserBookings()
      .subscribe(x => {
        this.bookingsResponses = x;

        for (let i = 0; i < this.bookingsResponses.length; i++) {
          this.bookings.push({
            userFullName: this.bookingsResponses[i].booking.user.fullName,
            date: this.dateService.prettyDate(this.bookingsResponses[i].booking.date),
            bookingType: this.bookingsResponses[i].booking.type
          });
        }
    
        if (this.bookingsResponses[0].booking.date != null) {
          this.bookings.sort(this.compare).reverse();
        } else {
          this.bookings[0].date = 'Permanent';
        }
    });
    
    this.profileService.behaviourSubject
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((res: any) => {
        this.bookings.forEach(x => x.userFullName = res.fullName);
      });
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  compare(a : BookingModel, b: BookingModel): number {
    if (a.date < b.date) {
      return -1;
    } else if (a.date > b.date) {
      return 1;
    }
    
    return 0;
  }

}
