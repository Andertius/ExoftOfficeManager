import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModulesModule } from '../material-modules/material-modules.module';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';

import { MainMeetingComponent } from './components/main-meeting/main-meeting.component';
import { MeeetingCalendarComponent } from './components/meeeting-calendar/meeeting-calendar.component'
import { NewMeetingComponent } from './components/new-meeting/new-meeting.component';;
import { YourMeetingsComponent } from './components/your-meetings/your-meetings.component';

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
