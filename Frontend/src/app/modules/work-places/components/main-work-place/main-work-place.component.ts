import { ErrorService } from 'src/app/shared/services/error.service';
import { UserService } from 'src/app/core/services/user.service';

import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ActivatedRoute, } from '@angular/router';

@Component({
    selector: 'app-main-work-place',
    templateUrl: './main-work-place.component.html',
    styleUrls: ['./main-work-place.component.scss']
})
export class MainWorkPlaceComponent implements OnInit, OnDestroy {
    private _unsubscribe$: Subject<void> = new Subject();
    private _userFirstName: string;

    public userFullName!: string;
    public id!: string;

    public get userFirstName(): string {
        return this._userFirstName.toUpperCase();
    }

    constructor(
        private activatedRoute: ActivatedRoute,
        private readonly _errorService: ErrorService) {
            this.activatedRoute.queryParamMap.subscribe(params => {
                this.userFullName = String(params.get('userFullName'));
                this.id = String(params.get('id'));
            });
        
            this._userFirstName = this.userFullName.split(' ')[0].toUpperCase();
        }

    public ngOnInit(): void {
        this._errorService.errorSubject
            .pipe(takeUntil(this._unsubscribe$))
            .subscribe(x => console.log(x));
    }

    public ngOnDestroy(): void {
        this._unsubscribe$.next();
        this._unsubscribe$.complete();
    }
}
