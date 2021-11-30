import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { User } from './core/models/user.model';
import { UserService } from './core/services/user.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
    private _unsubscribe$: Subject<void> = new Subject();

    public user!: User;

    constructor(
        private readonly _userService: UserService,
        private readonly _router: Router) { }

    public ngOnInit(): void {
        this._userService.dummyUserSubject
            .pipe(takeUntil(this._unsubscribe$))
            .subscribe(x => {
                this.user = x;

                if (this.user.avatar === null) {
                    this.user.avatar = '';
                }

                this._router.navigate(['work-place'], { queryParams: { id: this.user.id, userFullName: `${this.user.firstName} ${this.user.lastName}` } });
            });

        this._userService.getDummyUser().subscribe();
    }

    public ngOnDestroy(): void {
        this._unsubscribe$.next();
        this._unsubscribe$.complete();
    }
}
