import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewMeetingComponent } from './new-meeting/new-meeting.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import {MatIconModule} from '@angular/material/icon';



@NgModule({
  declarations: [
    NewMeetingComponent
  ],
  imports: [
    CommonModule,

    ReactiveFormsModule,

    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatChipsModule,
    MatIconModule
  ]
})
export class MeetingsModule { }
