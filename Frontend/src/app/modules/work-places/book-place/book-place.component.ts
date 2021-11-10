import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-book-place',
  templateUrl: './book-place.component.html',
  styleUrls: ['./book-place.component.scss']
})
export class BookPlaceComponent implements OnInit {

  bookingForm!: FormGroup;

  constructor() { }

  ngOnInit(): void {
    this.bookingForm = new FormGroup({
      time: new FormControl('', Validators.required),
      date: new FormControl('', Validators.required),
    });
  }

}
