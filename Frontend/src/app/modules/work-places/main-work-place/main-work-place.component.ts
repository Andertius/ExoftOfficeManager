import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { BookingService } from 'src/app/core/services/booking.service';

@Component({
  selector: 'app-main-work-place',
  templateUrl: './main-work-place.component.html',
  styleUrls: ['./main-work-place.component.scss']
})
export class MainWorkPlaceComponent implements OnInit {
  
  private unsubscribe$: Subject<void> = new Subject();

  firstUserName: string = "Alissa";

  constructor(private readonly bookingService: BookingService) { }

  ngOnInit(): void {
    this.bookingService.errorSubject
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(x => {
        alert(x);
      });
  }

}
