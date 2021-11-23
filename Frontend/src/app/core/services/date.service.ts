import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DateService {
    private _behaviourSubject$: Subject<Date> = new BehaviorSubject(new Date());

    public get behaviourSubject(): Subject<Date> {
        return this._behaviourSubject$;
    }

    public set behaviourSubject(value: any) {
        this._behaviourSubject$.next(value);
    }

    public prettyDate(date: Date): string {
        return `${this.parseMonth(new Date(date).getMonth())}` + ' ' +
               `${new Date(date).getDate()}` + ', ' +
               `${new Date(date).getFullYear()}`;
    }

    private parseMonth(month: number): string {
        let result: string = "";

        switch (month) {
        case 0:
            result = "Jan";
            break;

        case 1:
            result = "Feb";
            break;

        case 2:
            result = "Mar";
            break;

        case 3:
            result = "Apr";
            break;

        case 4:
            result = "May";
            break;

        case 5:
            result = "Jun";
            break;

        case 6:
            result = "Jul";
            break;
            
        case 7:
            result = "Aug";
            break;
            
        case 8:
            result = "Sep";
            break;
            
        case 9:
            result = "Oct";
            break;
            
        case 10:
            result = "Nov";
            break;
            
        case 11:
            result = "Dec";
            break;

        default:
            console.log('Invalid month');
        }

        return result;
    }
}
