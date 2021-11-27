import { Component, OnInit } from '@angular/core';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { FormControl, FormGroup } from '@angular/forms';
import { NewMeeting } from 'src/app/models/new-meeting.model';
import { MatChipInputEvent } from '@angular/material/chips';

@Component({
    selector: 'app-new-meeting',
    templateUrl: './new-meeting.component.html',
    styleUrls: ['./new-meeting.component.scss']
})
export class NewMeetingComponent implements OnInit {
    public selectable = true;
    public removable = true;
    public addOnBlur = true;

    public meeting!: NewMeeting;
    public meetingForm!: FormGroup;

    public readonly separatorKeysCodes = [ENTER, COMMA] as const;
    public required: string[] = new Array<string>();
    public nonRequired: string[] = new Array<string>();

    public ngOnInit(): void {
        this.meetingForm = new FormGroup({
            required: new FormControl(''),
            nonRequired: new FormControl(''),
            subject: new FormControl(''),
        });
    }

    public addRequired(event: MatChipInputEvent): void {
        const value = (event.value || '').trim();

        if (value &&
            this.required.find(x => x === value) === undefined &&
            this.nonRequired.find(x => x === value) === undefined)
        {
            this.required.push(value);
        }

        event.chipInput!.clear();
    }

    public removeRequired(fruit: string): void {
        const index = this.required.indexOf(fruit);

        if (index >= 0) {
            this.required.splice(index, 1);
        }
    }

    public addNonRequired(event: MatChipInputEvent): void {
        const value = (event.value || '').trim();

        if (value &&
            this.nonRequired.find(x => x === value) === undefined &&
            this.required.find(x => x === value) === undefined) {
            this.nonRequired.push(value);
        }

        event.chipInput!.clear();
    }

    public removeNonRequired(fruit: string): void {
        const index = this.nonRequired.indexOf(fruit);

        if (index >= 0) {
            this.nonRequired.splice(index, 1);
        }
    }

    public submit(): void {
        alert('yeet');
    }
}
