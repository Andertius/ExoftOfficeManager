import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main-meeting',
  templateUrl: './main-meeting.component.html',
  styleUrls: ['./main-meeting.component.scss']
})
export class MainMeetingComponent implements OnInit {

  firstUserName: string = "Alissa";

  constructor() { }

  ngOnInit(): void {
  }

}
