import { Component, OnInit } from '@angular/core';
import { DateService } from 'src/app/core/services/date.service';

@Component({
  selector: 'app-meeeting-calendar',
  templateUrl: './meeeting-calendar.component.html',
  styleUrls: ['./meeeting-calendar.component.scss']
})
export class MeeetingCalendarComponent implements OnInit {
  date: string = "";

  constructor(private readonly dateService: DateService,) { }

  ngOnInit(): void {
    this.date = this.dateService.prettyDate(new Date()).toUpperCase();
  }

}
