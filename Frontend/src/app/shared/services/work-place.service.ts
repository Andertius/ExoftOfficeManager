import { ErrorService } from "./error.service";

import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of, EMPTY } from "rxjs";
import { catchError } from "rxjs/operators";
import { WorkPlaceResponse } from "src/app/models/responses/work-placeRespnose.model";

@Injectable({
    providedIn: 'root'
})
export class WorkPlaceService {
    constructor(
        private readonly _http: HttpClient,
        private readonly _errorService: ErrorService)
    { }

    public getWorkPlaces(): WorkPlaceResponse[] {
        const workPlaces: WorkPlaceResponse[] = [];

        this._http.get<WorkPlaceResponse[]>('https://localhost:44377/WorkPlace/work-places/')
            .pipe(catchError(err => {
                if (err.status === 400) {
                    this._errorService.setErrorSubject(err.error.message);
                }

                return of([]);
            }))
            .subscribe(x => {
                for (let place of x) {
                    workPlaces.push(place)
                }
            }
        );

        return workPlaces;
    }

    public findWorkPlaceByPlaceNumber(place: number, floor: number): Observable<WorkPlaceResponse> {
        return this._http.get<WorkPlaceResponse>(`https://localhost:44377/WorkPlace/work-places/${place}/${floor}/work-place`)
            .pipe(catchError(err => {
                if (err.status === 400) {
                    this._errorService.setErrorSubject('err.error.message');
                }

                return EMPTY;
            }));
    }
}
