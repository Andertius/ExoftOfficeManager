import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './modules/auth/login/login.component';
import { ProfileComponent } from './modules/auth/profile/profile.component';
import { NewMeetingComponent } from './modules/meetings/new-meeting/new-meeting.component';
import { BookPlaceComponent } from './modules/work-places/book-place/book-place.component';
import { GreetingComponent } from './modules/work-places/greeting/greeting.component';
import { StatusInfoComponent } from './modules/work-places/status-info/status-info.component';

const routes: Routes = [
  { path: 'newMeeting', component: NewMeetingComponent },
  { path: 'login', component: LoginComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'greeting', component: GreetingComponent },
  { path: 'status-info', component: StatusInfoComponent },
  { path: 'book-place', component: BookPlaceComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
