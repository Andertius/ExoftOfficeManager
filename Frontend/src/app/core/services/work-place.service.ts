import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { WorkPlaceResponse } from "src/app/models/responses/work-placeRespnose.model";
import { DateService } from "./date.service";

@Injectable({
    providedIn: 'root'
})
export class WorkPlaceService {

    constructor(private readonly http: HttpClient) { }

    getWorkPlaces(): WorkPlaceResponse[] {
        const workPlaces: WorkPlaceResponse[] = [];

        this.http.get<WorkPlaceResponse[]>('https://localhost:44377/WorkPlace/work-places/')
            .subscribe(x => {
                for (let place of x) {
                    workPlaces.push(place)
                }
            }
        );

        return workPlaces;
    }

    findWorkPlaceByPlaceNumber(place: number, floor: number): Observable<WorkPlaceResponse> {
        return this.http.get<WorkPlaceResponse>(`https://localhost:44377/WorkPlace/work-places/${place}/${floor}/work-place`);
    }
}
