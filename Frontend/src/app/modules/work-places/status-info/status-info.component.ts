import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-status-info',
  templateUrl: './status-info.component.html',
  styleUrls: ['./status-info.component.scss'],
  host: { class: 'app-status-info' }
})
export class StatusInfoComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
