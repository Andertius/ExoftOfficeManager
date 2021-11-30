import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { tap } from "rxjs/operators";
import { UserResponse } from "src/app/core/models/user-response.model";
import { User } from "../models/user.model";

@Injectable({
    providedIn: 'root'
})
export class UserService {
    private _dummyUserSubject$: Subject<User> = new Subject();

    public get dummyUserSubject(): Subject<User> {
        return this._dummyUserSubject$;
    }

    public setDummyUserSubject(value: User) {
        this._dummyUserSubject$.next(value);
    }

    constructor(private readonly _http: HttpClient) {
        this.getDummyUser();
    }

    public getDummyUser(): Observable<UserResponse> {
        return this._http
            .get<UserResponse>("https://localhost:44377/User/users/dummy-user")
            .pipe(tap(x => this.dummyUserSubject.next(this.convertResponseToModel(x))));
    }

    private convertResponseToModel(res: UserResponse): User
    {
        return {
            id: res.id,
            avatar: res.avatar,
            firstName: res.fullName.split(' ')[0],
            lastName: res.fullName.split(' ')[1],
            email: res.email,
            status: 'status',
        };
    }
}
