import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class ErrorService {
    private _errorSubject$: Subject<string> = new Subject();

    public get errorSubject(): Subject<string> {
        return this._errorSubject$;
    }

    public setErrorSubject(value: string) {
        this._errorSubject$.next(value);
    }
}
