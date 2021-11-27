import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModulesModule } from '../modules/material-modules/material-modules.module';
import { ReactiveFormsModule } from '@angular/forms';

import { GreetingComponent } from './components/greeting/greeting.component';
import { EditProfileComponent } from './components/edit-profile/edit-profile.component';
import { ProfileComponent } from './components/profile/profile.component';
import { DatePickerComponent } from './components/date-picker/date-picker.component';

import { InitialsPipe } from './pipes/initials.pipe';

@NgModule({
    declarations: [
        GreetingComponent,
        EditProfileComponent,
        InitialsPipe,
        ProfileComponent,
        DatePickerComponent,
    ],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MaterialModulesModule,
    ],
    exports: [
        GreetingComponent,
        EditProfileComponent,
        InitialsPipe,
    ]
})
export class SharedModule { }
