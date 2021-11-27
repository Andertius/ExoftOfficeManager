import { Component, ElementRef, Inject, OnInit } from '@angular/core';
import { MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'app-date-picker',
    templateUrl: './date-picker.component.html',
    styleUrls: ['./date-picker.component.scss']
})
export class DatePickerComponent implements OnInit {
    public triggerElementRef: ElementRef;
    public selectedDate: Date = new Date();

    constructor(
        private readonly _matDialogRef: MatDialogRef<DatePickerComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { pickedDate: Date, trigger: ElementRef }) {
        this.triggerElementRef = data.trigger;
    }

    public ngOnInit(): void {
        const matDialogConfig: MatDialogConfig = new MatDialogConfig();
        const rect = this.triggerElementRef.nativeElement.getBoundingClientRect();
        matDialogConfig.position = { left: `${rect.left}px`, top: `${rect.bottom - 35}px` };
        this._matDialogRef.updatePosition(matDialogConfig.position);
    }

    public close(): void {
        this._matDialogRef.close(this.selectedDate);
    }
}
