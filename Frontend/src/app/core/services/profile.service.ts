import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { EditProfileResult } from "src/app/models/edit-profile-result.model";
import { UpdateUserRequest } from "src/app/models/requests/update-user-request.model";

@Injectable({
    providedIn: 'root'
})
export class ProfileService {
    private _profileSubject$: Subject<EditProfileResult> = new Subject();

    public get profileSubject(): Subject<EditProfileResult> {
        return this._profileSubject$;
    }

    constructor(private readonly _http: HttpClient) { }

    public setProfileSubject(value: EditProfileResult): void {
        this._profileSubject$.next(value);
    }

    public updateUser(id: string, userRequest: UpdateUserRequest): void {
        this._http
            .put(`https://localhost:44377/User/users/${id}/update-user`, userRequest)
            .subscribe();
    }
}
