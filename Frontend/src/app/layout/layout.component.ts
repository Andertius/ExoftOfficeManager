import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {

  @Input() userFullName!: string;

  links: Array<string> = [
    'newMeeting',
    'login',
    'book-place',
    'work-place',
    'meeting',
  ];

  activeLink = "";

  constructor() { }

  ngOnInit(): void {
  }

}
