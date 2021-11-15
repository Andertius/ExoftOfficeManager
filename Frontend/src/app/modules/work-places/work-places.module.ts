import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { StatusInfoComponent } from './status-info/status-info.component';
import { BookPlaceComponent } from './book-place/book-place.component';

import { MainWorkPlaceComponent } from './main-work-place/main-work-place.component';
import { MaterialModulesModule } from '../material-modules/material-modules.module';
import { TablesComponent } from './tables/tables.component';
import { CurrentDayBookingsComponent } from './current-day-bookings/current-day-bookings.component';
import { YourBookingsComponent } from './your-bookings/your-bookings.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    StatusInfoComponent,
    MainWorkPlaceComponent,
    BookPlaceComponent,
    TablesComponent,
    CurrentDayBookingsComponent,
    YourBookingsComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MaterialModulesModule,
    SharedModule,
  ],
})
export class WorkPlacesModule { }
