import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { MatCalendar } from '@angular/material/datepicker';
import { MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-date-picker',
  templateUrl: './date-picker.component.html',
  styleUrls: ['./date-picker.component.scss']
})
export class DatePickerComponent implements OnInit {

  triggerElementRef: ElementRef;
  selectedDate: Date = new Date();

  constructor(
    private readonly _matDialogRef: MatDialogRef<DatePickerComponent>,
    public readonly dialogRef: MatDialogRef<DatePickerComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { pickedDate: Date, trigger: ElementRef }) {
      this.triggerElementRef = data.trigger;
    }

  ngOnInit(): void {
    const matDialogConfig: MatDialogConfig = new MatDialogConfig();
    const rect = this.triggerElementRef.nativeElement.getBoundingClientRect();
    matDialogConfig.position = { left: `${rect.left}px`, top: `${rect.bottom - 35}px` };
    this._matDialogRef.updatePosition(matDialogConfig.position);
  }

  close(): void {
    this._matDialogRef.close(this.selectedDate);
  }

}
