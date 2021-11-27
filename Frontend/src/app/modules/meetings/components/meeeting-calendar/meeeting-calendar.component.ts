import { DateService } from 'src/app/shared/services/date.service';

import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-meeeting-calendar',
    templateUrl: './meeeting-calendar.component.html',
    styleUrls: ['./meeeting-calendar.component.scss']
})
export class MeeetingCalendarComponent implements OnInit {
    public date: string = "";

    constructor(private readonly _dateService: DateService) { }

    public ngOnInit(): void {
        this.date = this._dateService.prettyDate(new Date()).toUpperCase();
    }
}
