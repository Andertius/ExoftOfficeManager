import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {

  links: Array<string> = [
    'newMeeting',
    'login',
    'profile',
    'greeting',
    'status-info',
    'book-place',
    'work-place',
  ];

  activeLink = "";

  constructor() { }

  ngOnInit(): void {
  }

}
