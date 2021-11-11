import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewMeetingComponent } from './new-meeting/new-meeting.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModulesModule } from '../material-modules/material-modules.module';



@NgModule({
  declarations: [
    NewMeetingComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MaterialModulesModule
  ]
})
export class MeetingsModule { }
