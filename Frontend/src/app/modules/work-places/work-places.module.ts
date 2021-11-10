import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { GreetingComponent } from './greeting/greeting.component';
import { StatusInfoComponent } from './status-info/status-info.component';
import { BookPlaceComponent } from './book-place/book-place.component';

import { MatRadioModule } from '@angular/material/radio';

@NgModule({
  declarations: [
    GreetingComponent,
    StatusInfoComponent,
    BookPlaceComponent
  ],
  imports: [
    CommonModule,

    ReactiveFormsModule,

    MatRadioModule,
  ]
})
export class WorkPlacesModule { }
