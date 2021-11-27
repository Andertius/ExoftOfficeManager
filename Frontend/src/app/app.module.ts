import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AuthModule } from './modules/auth/auth.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'
import { LayoutModule } from './modules/layout/layout.module';
import { MeetingsModule } from './modules/meetings/meetings.module';
import { MaterialModulesModule } from './modules/material-modules/material-modules.module';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './shared/shared.module';
import { WorkPlacesModule } from './modules/work-places/work-places.module';

import { AppComponent } from './app.component';

@NgModule({
    declarations: [
        AppComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        ReactiveFormsModule,
        WorkPlacesModule,
        MeetingsModule,
        AuthModule,
        MaterialModulesModule,
        HttpClientModule,
        SharedModule,
        LayoutModule,
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
