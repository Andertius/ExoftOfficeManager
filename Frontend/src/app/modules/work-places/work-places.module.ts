import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { GreetingComponent } from './greeting/greeting.component';
import { StatusInfoComponent } from './status-info/status-info.component';
import { BookPlaceComponent } from './book-place/book-place.component';

import { MainWorkPlaceComponent } from './main-work-place/main-work-place.component';
import { MaterialModulesModule } from '../material-modules/material-modules.module';
import { TablesComponent } from './tables/tables.component';

@NgModule({
  declarations: [
    GreetingComponent,
    StatusInfoComponent,
    MainWorkPlaceComponent,
    BookPlaceComponent,
    TablesComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MaterialModulesModule,
  ],
})
export class WorkPlacesModule { }
