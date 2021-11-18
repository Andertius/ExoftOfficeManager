import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-greeting',
  templateUrl: './greeting.component.html',
  styleUrls: ['./greeting.component.scss'],
  host: { class: 'app-greeting' }
})
export class GreetingComponent implements OnInit {

  @Input() firstName!: string;
  greetingMessage: string = "Here you can all see working places in the office and book any of the available ones!";

  constructor() { }

  ngOnInit(): void {
    this.firstName = this.firstName.toUpperCase();
  }

}
