import { Injectable } from "@angular/core";
import { BehaviorSubject, Subject } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class ProfileService {
    private _behaviourSubject$: Subject<{ avatar: string, fullName: string }> = new BehaviorSubject({avatar: "", fullName: "Alissa",});

    public get behaviourSubject() {
        return this._behaviourSubject$;
    }

    public set behaviourSubject(value: any) {
        this._behaviourSubject$.next(value);
    }
}
