import { EditProfileComponent } from '../../shared/components/edit-profile/edit-profile.component';

import { ProfileService } from '../../core/services/profile.service';

import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { EditProfileResult } from '../../models/edit-profile-result.model';

@Component({
    selector: 'app-layout',
    templateUrl: './layout.component.html',
    styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit, OnDestroy {
    @Input() userFullName!: string;

    private _unsubscribe$: Subject<void> = new Subject();

    public links: Array<string> = [
        'book-place',
        'work-place',
    ];

    public activeLink = "";

    constructor(
        private readonly _dialog: MatDialog,
        private readonly _profileService: ProfileService)
    { }

    public ngOnInit(): void {
        this._profileService.profileSubject
            .pipe(takeUntil(this._unsubscribe$))
            .subscribe((res: EditProfileResult) => {
                this.userFullName = res.fullName;
            });

        this.userFullName = 'Alissa White-Gluz';
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
                    firstName: "Alissa",
                    lastName: "White-Gluz",
                    email: "alissa@archenemy.info",
                    status: "Status",
                    avatar: "",
                },
                prevName: this.userFullName,
            }
        });

        dialogRef.afterClosed().subscribe(_ => console.log('The dialog was closed'));
    }
}
