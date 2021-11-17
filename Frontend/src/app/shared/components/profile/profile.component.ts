import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserModel } from 'src/app/models/user.model';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProfileService } from 'src/app/core/services/profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  user: UserModel = {
    firstName: 'Phil',
    lastName: 'Anselmo',
    email: 'panteraZeBest@pantera.metal',
    status: 'Status',
    avatar: '',
  }

  userForm!: FormGroup;

  emailRegex: RegExp =/(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])/;
  
  constructor(
    private readonly profileService: ProfileService,
    public dialogRef: MatDialogRef<ProfileComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserModel) { }

  ngOnInit(): void {
    const { firstName, lastName, email, status } = this.user;

    this.userForm = new FormGroup({
      firstName: new FormControl(firstName, Validators.required),
      lastName: new FormControl(lastName, Validators.required),
      email: new FormControl(email, Validators.required),
      status: new FormControl(status),
    });

    this.userForm.valueChanges.subscribe(({firstName, lastName}) => {
      this.profileService.behaviourSubject = {
        avatar: "",
        fullName: `${firstName} ${lastName}`,
      };
    })
  }

  submit(): void {
    this.profileService.behaviourSubject = {
      avatar: "",
      fullName: `${this.userForm.value.firstName} ${this.userForm.value.lastName}`,
    };
    this.dialogRef.close();
  }

}
