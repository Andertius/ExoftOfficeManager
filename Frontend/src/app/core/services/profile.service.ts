import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { EditProfileResult } from "src/app/models/edit-profile-result.model";

@Injectable({
    providedIn: 'root'
})
export class ProfileService {
    private _profileSubject$: Subject<EditProfileResult> = new Subject();

    public get profileSubject(): Subject<EditProfileResult> {
        return this._profileSubject$;
    }

    public setProfileSubject(value: EditProfileResult): void {
        this._profileSubject$.next(value);
    }
}
