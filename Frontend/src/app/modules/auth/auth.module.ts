import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModulesModule } from '../material-modules/material-modules.module';
import { ReactiveFormsModule } from '@angular/forms';

import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { LoginComponent } from './components/login/login.component';

@NgModule({
    declarations: [
        LoginComponent,
        ForgotPasswordComponent,
    ],
    imports: [
        CommonModule,

        ReactiveFormsModule,

        MaterialModulesModule
    ]
})
export class AuthModule { }
