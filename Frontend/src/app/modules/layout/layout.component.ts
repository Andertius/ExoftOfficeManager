import { EditProfileComponent } from '../../shared/components/edit-profile/edit-profile.component';

import { ProfileService } from '../../core/services/profile.service';

import { AfterViewInit, Component, Input, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { EditProfileResult } from '../../models/edit-profile-result.model';
import { User } from 'src/app/core/models/user.model';
import { Router } from '@angular/router';

@Component({
    selector: 'app-layout',
    templateUrl: './layout.component.html',
    styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit, AfterViewInit, OnDestroy {
    @Input() user!: User;

    private _unsubscribe$: Subject<void> = new Subject();

    public activeLink = "";

    constructor(
        private readonly _dialog: MatDialog,
        private readonly _profileService: ProfileService,
        private readonly _router: Router)
    { }

    public ngOnInit(): void {
        this._profileService.profileSubject
            .pipe(takeUntil(this._unsubscribe$))
            .subscribe((res: EditProfileResult) => {
                this.user.firstName = res.fullName.split(' ')[0];
                this.user.lastName = res.fullName.split(' ')[1];

                if (this.user.lastName === undefined) {
                    this.user.lastName = "";
                }

                this.user.email = res.email;
            });
    }

    public ngAfterViewInit(): void {
        if (this.user.lastName === undefined) {
            this.user.lastName = "";
        }
    }

    public ngOnDestroy(): void {
        this._unsubscribe$.next();
        this._unsubscribe$.complete();
    }

    public openDialog(): void {
        const dialogRef = this._dialog.open(EditProfileComponent, {
            width: '500px',
            data: {
                user: {
                    id: this.user.id,
                    firstName: this.user.firstName,
                    lastName: this.user.lastName,
                    email: this.user.email,
                    status: "Status",
                    avatar: this.user.avatar,
                },
                prevName: this.user.firstName + ' ' + this.user.lastName,
            }
        });

        dialogRef.afterClosed().subscribe(_ => console.log('The dialog was closed'));
    }

    public changeToWorkPlace(event: Event): void {
        this.activeLink = 'work-place';
        this._router.navigate(['work-place'], { queryParams: { id: this.user.id, userFullName: `${this.user.firstName} ${this.user.lastName}` } });
        console.log(this._router.url);
    }

    public changeToMeeting(event: Event): void {
        this.activeLink = 'meeting';
        this._router.navigate(['meeting'], { queryParams: { id: this.user.id, userFullName: `${this.user.firstName} ${this.user.lastName}` } });
    }
}
