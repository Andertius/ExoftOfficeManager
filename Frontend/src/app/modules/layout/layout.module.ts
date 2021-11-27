import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModulesModule } from '../material-modules/material-modules.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { AppRoutingModule } from 'src/app/app-routing.module';

import { LayoutComponent } from './layout.component';

@NgModule({
    declarations: [
        LayoutComponent,
    ],
    imports: [
        CommonModule,
        MaterialModulesModule,
        SharedModule,
        AppRoutingModule,
    ],
    exports: [
        LayoutComponent,
    ]
})
export class LayoutModule { }
