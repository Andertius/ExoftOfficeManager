import { Component, OnDestroy, OnInit } from '@angular/core';
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

    public title = 'Frontend';
    public user!: User;

    constructor(private readonly _userService: UserService) { }

    public ngOnInit(): void {
        this._userService.dummyUserSubject
            .pipe(takeUntil(this._unsubscribe$))
            .subscribe(x => {
                this.user = x;

                if (this.user.avatar === null) {
                    this.user.avatar = '';
                }
                
                sessionStorage.setItem("sessionUserFullName", `${this.user.firstName} ${this.user.lastName}`);
                sessionStorage.setItem("sessionUserId", this.user.id);
            });

        this._userService.getDummyUser().subscribe();
    }

    public ngOnDestroy(): void {
        this._unsubscribe$.next();
        this._unsubscribe$.complete();
    }
}
