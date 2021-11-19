import { Component, OnInit } from '@angular/core';
import { DateService } from 'src/app/core/services/date.service';

@Component({
  selector: 'app-tables',
  templateUrl: './tables.component.html',
  styleUrls: ['./tables.component.scss'],
  host: { class: 'app-tables' }
})
export class TablesComponent implements OnInit {

  date: string = "";

  constructor(private readonly dateService: DateService) { }

  ngOnInit(): void {
    this.date = this.dateService.prettyDate(new Date()).toUpperCase();
  }

}
