import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModulesModule } from '../material-modules/material-modules.module';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';

import { BookPlaceComponent } from './components/book-place/book-place.component';
import { CurrentDayBookingsComponent } from './components/current-day-bookings/current-day-bookings.component';
import { MainWorkPlaceComponent } from './components/main-work-place/main-work-place.component';
import { StatusInfoComponent } from './components/status-info/status-info.component';
import { TablesComponent } from './components/tables/tables.component';
import { YourBookingsComponent } from './components/your-bookings/your-bookings.component';

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
    exports: [
        MainWorkPlaceComponent,
    ]
})
export class WorkPlacesModule { }
