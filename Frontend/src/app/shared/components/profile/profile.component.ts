import { Component, ElementRef, Inject, OnInit } from '@angular/core';
import { MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DateService } from 'src/app/core/services/date.service';
import { BookingType } from 'src/app/models/enums/booking-type.enum';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  
  private readonly triggerElementRef: ElementRef;

  constructor(@Inject(MAT_DIALOG_DATA) public data: {
      username: string,
      workPlace: { tableNumber: number, floorNumber: number },
      date: Date,
      bookingType: BookingType,
      trigger: ElementRef
    },
  private readonly _matDialogRef: MatDialogRef<ProfileComponent>,
  private readonly dateService: DateService) {
    
    this.triggerElementRef = data.trigger;
  }

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
    return this.dateService.prettyDate(this.data.date).toUpperCase();
  }

  ngOnInit(): void {
    const matDialogConfig: MatDialogConfig = new MatDialogConfig();
    const rect = this.triggerElementRef.nativeElement.getBoundingClientRect();
    matDialogConfig.position = { left: `${rect.left}px`, top: `${rect.bottom - 50}px` };
    this._matDialogRef.updatePosition(matDialogConfig.position);
  }

}
