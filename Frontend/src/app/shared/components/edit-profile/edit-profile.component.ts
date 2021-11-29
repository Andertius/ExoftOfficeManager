import { ProfileService } from 'src/app/core/services/profile.service';

import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/core/models/user.model';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'app-edit-profile',
    templateUrl: './edit-profile.component.html',
    styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent implements OnInit {
    public user: User;
    public userForm!: FormGroup;
    public emailRegex: RegExp = /(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])/;

    constructor(
        private readonly _profileService: ProfileService,
        private readonly _dialogRef: MatDialogRef<EditProfileComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { user: User, prevName: string }) {
        this.user = data.user;
    }

    public ngOnInit(): void {
        const { firstName, lastName, email, status } = this.user;

        this.userForm = new FormGroup({
            firstName: new FormControl(firstName, Validators.required),
            lastName: new FormControl(lastName, Validators.required),
            email: new FormControl(email, Validators.required),
            status: new FormControl(status),
        });
    }

    public submit(): void {
        this._profileService.setProfileSubject({
            avatar: this.user.avatar,
            fullName: `${this.userForm.value.firstName} ${this.userForm.value.lastName}`,
            prevName: this.data.prevName,
        });
        this._dialogRef.close();
    }

    public close(): void {
        this._dialogRef.close();
    }
}
