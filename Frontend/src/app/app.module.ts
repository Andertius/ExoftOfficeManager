import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ReactiveFormsModule } from '@angular/forms';

import { LayoutComponent } from './layout/layout.component';

import { WorkPlacesModule } from './modules/work-places/work-places.module';
import { MeetingsModule } from './modules/meetings/meetings.module';
import { AuthModule } from './modules/auth/auth.module';
import { MaterialModulesModule } from './modules/material-modules/material-modules.module';

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    WorkPlacesModule,
    MeetingsModule,
    AuthModule,
    MaterialModulesModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
