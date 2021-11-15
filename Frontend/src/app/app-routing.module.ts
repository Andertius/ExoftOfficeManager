import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './modules/auth/login/login.component';
import { MainMeetingComponent } from './modules/meetings/main-meeting/main-meeting.component';
import { NewMeetingComponent } from './modules/meetings/new-meeting/new-meeting.component';
import { BookPlaceComponent } from './modules/work-places/book-place/book-place.component';
import { MainWorkPlaceComponent } from './modules/work-places/main-work-place/main-work-place.component';

const routes: Routes = [
  { path: 'newMeeting', component: NewMeetingComponent },
  { path: 'login', component: LoginComponent },
  { path: 'book-place', component: BookPlaceComponent },
  { path: 'work-place', component: MainWorkPlaceComponent },
  { path: 'meeting', component: MainMeetingComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
