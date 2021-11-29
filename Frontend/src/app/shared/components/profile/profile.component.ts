import { DateService } from 'src/app/shared/services/date.service';

import { Component, ElementRef, Inject, OnInit } from '@angular/core';
import { MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BookingType } from 'src/app/models/enums/booking-type.enum';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

    private readonly _triggerElementRef: ElementRef;

    public get bookingText(): string {
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

    public get date(): string {
        return this._dateService.prettyDate(this.data.date).toUpperCase();
    }

    constructor(@Inject(MAT_DIALOG_DATA) public data: {
        username: string,
        workPlace: { tableNumber: number, floorNumber: number },
        date: Date,
        bookingType: BookingType,
        avatar: string,
        trigger: ElementRef,
    },
        private readonly _matDialogRef: MatDialogRef<ProfileComponent>,
        private readonly _dateService: DateService) {

        this._triggerElementRef = data.trigger;
    }

    public ngOnInit(): void {
        const matDialogConfig: MatDialogConfig = new MatDialogConfig();
        const rect = this._triggerElementRef.nativeElement.getBoundingClientRect();
        matDialogConfig.position = { left: `${rect.left}px`, top: `${rect.bottom - 50}px` };
        this._matDialogRef.updatePosition(matDialogConfig.position);
    }
}
