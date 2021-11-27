import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';

@Component({
    selector: 'app-forgot-password',
    templateUrl: './forgot-password.component.html',
    styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {
    public passwordForm!: FormGroup;
    public passwordRegex: RegExp = /^(?=\D*\d)(?=[^a-z]*[a-z])(?=[^A-Z]*[A-Z]).{8,30}$/;

    public ngOnInit(): void {
        this.passwordForm = new FormGroup({
            username: new FormControl('', Validators.required),
            password: new FormControl('', [Validators.required, this.validatePassword.bind(this)])
        });
    }

    public submit(): void {
        alert('oooh yeah');
    }

    private validatePassword(control: FormControl): ValidationErrors | null {
        if (!!control.value) {
            return this.passwordRegex.test(control.value) ? null : { invalidPassword: true };
        }

        return null;
    }
}
