import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GreetingComponent } from './components/greeting/greeting.component';
import { MaterialModulesModule } from '../modules/material-modules/material-modules.module';
import { EditProfileComponent } from './components/edit-profile/edit-profile.component';
import { ReactiveFormsModule } from '@angular/forms';
import { InitialsPipe } from './pipes/initials.pipe';
import { ProfileComponent } from './components/profile/profile.component';
import { DatePickerComponent } from './components/date-picker/date-picker.component';

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
