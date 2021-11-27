import { ProfileService } from 'src/app/core/services/profile.service';

import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { EditProfileResult } from 'src/app/models/edit-profile-result.model';

@Component({
    selector: 'app-greeting',
    templateUrl: './greeting.component.html',
    styleUrls: ['./greeting.component.scss'],
    host: { class: 'app-greeting' }
})
export class GreetingComponent implements OnInit, OnDestroy {
    @Input() firstName!: string;

    private _unsubscribe$: Subject<void> = new Subject();

    public greetingMessage: string = "";

    constructor(private readonly _profileService: ProfileService) { }

    ngOnInit(): void {
        this.firstName = this.firstName.toUpperCase();

        this._profileService.profileSubject
            .pipe(takeUntil(this._unsubscribe$))
            .subscribe((res: EditProfileResult) => this.firstName = res.fullName.split(' ')[0]);
    }

    public ngOnDestroy(): void {
        this._unsubscribe$.next();
        this._unsubscribe$.complete();
    }
}
