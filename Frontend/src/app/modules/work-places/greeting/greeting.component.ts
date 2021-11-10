import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-greeting',
  templateUrl: './greeting.component.html',
  styleUrls: ['./greeting.component.scss']
})
export class GreetingComponent implements OnInit {

  firstName: string = "Greeting"
  greetingMessage: string = "Here you can all see working places in the office and book any of the available ones!";

  constructor() { }

  ngOnInit(): void {
    this.firstName = this.firstName.toUpperCase();
  }

}
