import { Injectable } from "@angular/core";
import { BehaviorSubject, Subject } from "rxjs";
import { EditProfileResultModel } from "src/app/models/edit-profile-result.model";

@Injectable({
    providedIn: 'root'
})
export class ProfileService {
    private _behaviourSubject$: Subject<EditProfileResultModel> = new BehaviorSubject({avatar: "", fullName: "Alissa", prevName: "Alissa"});

    public get behaviourSubject(): Subject<EditProfileResultModel> {
        return this._behaviourSubject$;
    }

    public set behaviourSubject(value: any) {
        this._behaviourSubject$.next(value);
    }
}
