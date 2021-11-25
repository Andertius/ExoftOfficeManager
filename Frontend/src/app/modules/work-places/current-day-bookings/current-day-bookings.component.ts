import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { BookingService } from 'src/app/core/services/booking.service';
import { DateService } from 'src/app/core/services/date.service';
import { ProfileService } from 'src/app/core/services/profile.service';
import { BookingModel } from 'src/app/models/booking.model';
import { EditProfileResultModel } from 'src/app/models/edit-profile-result.model';
import { BookingType } from 'src/app/models/enums/booking-type.enum';

@Component({
  selector: 'app-current-day-bookings',
  templateUrl: './current-day-bookings.component.html',
  styleUrls: ['./current-day-bookings.component.scss']
})
export class CurrentDayBookingsComponent implements OnInit {
  
  private unsubscribe$: Subject<void> = new Subject();
  
  bookings: BookingModel[] = [];

  constructor(
    private readonly profileService: ProfileService,
    private readonly bookingService: BookingService,
    private readonly dateService: DateService) { }

  public get bookingType(): typeof BookingType {
    return BookingType;
  }

  ngOnInit(): void {

    this.profileService.behaviourSubject
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((res: EditProfileResultModel) => {
        this.bookings.forEach(x => x.userFullName == res.prevName ? x.userFullName = res.fullName : '');
      });

    this.dateService.behaviourSubject
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(res => {
        res.setHours(3);
        this.bookingService.getSpecificDayBookings(res).subscribe();
      })

    this.bookingService.behaviourSubject1
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(res => {
        this.bookings = res;
      })
    
    this.bookingService.getSpecificDayBookings(new Date());
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
