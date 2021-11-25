import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BookingService } from 'src/app/core/services/booking.service';
import { BookingModel } from 'src/app/models/booking.model';
import { BookingType } from 'src/app/models/enums/booking-type.enum';
import { AddBookingRequest } from 'src/app/models/requests/addBookingRequest.model';

@Component({
  selector: 'app-book-place',
  templateUrl: './book-place.component.html',
  styleUrls: ['./book-place.component.scss']
})
export class BookPlaceComponent implements OnInit {

  bookingForm!: FormGroup;
  
  public get bookingType(): typeof BookingType {
    return BookingType;
  }

  constructor(public dialogRef: MatDialogRef<BookPlaceComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {
      request: AddBookingRequest,
      userFullName: string,
      tableNumber: number,
      floorNumber: number,
      bookings: BookingModel[],
     },
    private readonly bookingService: BookingService
  ) { }

  ngOnInit(): void {
    this.bookingForm = new FormGroup({
      type: new FormControl('', Validators.required),
      date: new FormControl('', Validators.required),
    });
  }

  close(): void {
    this.dialogRef.close();
  }

  submitButton(): void {
    this.data.request.bookingType = this.bookingForm.value.type;
    this.bookingForm.value.date.setHours(3);
    this.data.request.bookingDate = this.bookingForm.value.date.toISOString().split('T')[0];    

    if (!this.validateBooking()) {
      return;
    }

    this.bookingService.addBooking(this.data.request);

    this.bookingService.behaviourSubject = {
      date: this.data.request.bookingDate,
      userFullName: this.data.userFullName,
      bookingType: this.data.request.bookingType,
      tableNumber: this.data.tableNumber,
      floorNumber: this.data.floorNumber,
    };

    this.dialogRef.close();
  }

  validateBooking(): boolean {
    const today = new Date();
    today.setHours(3);
    debugger;
    const todayString = today.toISOString().split('T')[0];

    if (this.data.request.bookingDate < todayString) {
      alert("You cannot travel back in time!");
      return false;
    } else if (this.data.bookings.filter(x => x.userFullName === this.data.userFullName).length !== 0) {
      alert("You you have already booked a table for this day!");
      return false;
    }

    return true;
  }

}
