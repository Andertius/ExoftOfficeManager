import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NewMeetingComponent } from './new-meeting/new-meeting.component';

import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModulesModule } from '../material-modules/material-modules.module';
import { MainMeetingComponent } from './main-meeting/main-meeting.component';
import { SharedModule } from 'src/app/shared/shared.module';



@NgModule({
  declarations: [
    NewMeetingComponent,
    MainMeetingComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MaterialModulesModule,
    SharedModule,
  ]
})
export class MeetingsModule { }
