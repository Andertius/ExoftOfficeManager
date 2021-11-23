import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NewMeetingComponent } from './new-meeting/new-meeting.component';

import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModulesModule } from '../material-modules/material-modules.module';
import { MainMeetingComponent } from './main-meeting/main-meeting.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { MeeetingCalendarComponent } from './meeeting-calendar/meeeting-calendar.component';
import { YourMeetingsComponent } from './your-meetings/your-meetings.component';



@NgModule({
  declarations: [
    NewMeetingComponent,
    MainMeetingComponent,
    MeeetingCalendarComponent,
    YourMeetingsComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MaterialModulesModule,
    SharedModule,
  ],
  exports: [    
    MainMeetingComponent,
  ]
})
export class MeetingsModule { }
