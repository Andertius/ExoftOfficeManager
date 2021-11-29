import { ErrorService } from 'src/app/shared/services/error.service';

import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
    selector: 'app-main-work-place',
    templateUrl: './main-work-place.component.html',
    styleUrls: ['./main-work-place.component.scss']
})
export class MainWorkPlaceComponent implements OnInit, OnDestroy {
    userFirstName = "Alissa";

    private _unsubscribe$: Subject<void> = new Subject();

    constructor(private readonly _errorService: ErrorService) { }

    public ngOnInit(): void {
        this._errorService.errorSubject
            .pipe(takeUntil(this._unsubscribe$))
            .subscribe(x => alert(x));
    }

    public ngOnDestroy(): void {
        this._unsubscribe$.next();
        this._unsubscribe$.complete();
    }
}
