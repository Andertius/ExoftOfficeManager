import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './modules/auth/login/login.component';
import { ProfileComponent } from './modules/auth/profile/profile.component';
import { NewMeetingComponent } from './modules/meetings/new-meeting/new-meeting.component';

const routes: Routes = [
  { path: 'newMeeting', component: NewMeetingComponent },
  { path: 'login', component: LoginComponent },
  { path: 'profile', component: ProfileComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
