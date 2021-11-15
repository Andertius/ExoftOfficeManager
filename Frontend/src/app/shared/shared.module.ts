import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GreetingComponent } from './components/greeting/greeting.component';
import { MaterialModulesModule } from '../modules/material-modules/material-modules.module';
import { ProfileComponent } from './components/profile/profile.component';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    GreetingComponent,
    ProfileComponent,
  ],
  imports: [
    CommonModule,

    ReactiveFormsModule,
    
    MaterialModulesModule,
  ],
  exports: [
    GreetingComponent,
    ProfileComponent,
  ]
})
export class SharedModule { }
