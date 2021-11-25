import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './modules/auth/login/login.component';

import { MainMeetingComponent } from './modules/meetings/main-meeting/main-meeting.component';
import { MainWorkPlaceComponent } from './modules/work-places/main-work-place/main-work-place.component';

const routes: Routes = [
  { path: 'work-place', component: MainWorkPlaceComponent },
  { path: 'meeting', component: MainMeetingComponent },
  { path: 'login', component: LoginComponent },
  { path: '', pathMatch: 'full', redirectTo: 'work-place' },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
