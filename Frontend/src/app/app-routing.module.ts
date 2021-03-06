import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './modules/auth/components/login/login.component';
import { MainMeetingComponent } from './modules/meetings/components/main-meeting/main-meeting.component';
import { MainWorkPlaceComponent } from './modules/work-places/components/main-work-place/main-work-place.component';

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
