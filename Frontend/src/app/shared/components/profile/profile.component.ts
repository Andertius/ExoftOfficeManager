import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DateService } from 'src/app/core/services/date.service';
import { WorkPlaceModel } from 'src/app/models/work-place.model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: {
    username: string,
    workPlace: { tableNumber: number, floorNumber: number },
    date: Date,
    bookingType: number
  },
  private readonly dateService: DateService) { }

  get bookingText(): string {
    switch (this.data.bookingType) {
      case 1:
        return "Booked for the whole day";

      case 2:
        return "Booked the place for good";

      case 3:
        return "Booked from 10 AM to 2 PM";

      case 4:
        return "Booked from 2 PM to 6 PM";

      default:
        return "";
    }
  }

  get date(): string {
    return this.dateService.prettyDate(this.data.date);
  }

  ngOnInit(): void {
  }

}
