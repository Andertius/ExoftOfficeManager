import { Component, OnInit } from '@angular/core';
import { LoginModel } from 'src/app/models/login.model';
import { FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
    public model: LoginModel = {
        username: "",
        password: "",
    }

    public loginForm!: FormGroup;
    public passwordRegex: RegExp = /^(?=\D*\d)(?=[^a-z]*[a-z])(?=[^A-Z]*[A-Z]).{8,30}$/;

    public ngOnInit(): void {
        this.loginForm = new FormGroup({
            username: new FormControl('', Validators.required),
            password: new FormControl('', [Validators.required, this.validatePassword.bind(this)]),
        });
    }

    public submit(): void {
        alert('і шо тепер');
    }

    private validatePassword(control: FormControl): ValidationErrors | null {
        if (!!control.value) {
            return this.passwordRegex.test(control.value) ? null : { invalidPassword: true };
        }

        return null;
    }
}
