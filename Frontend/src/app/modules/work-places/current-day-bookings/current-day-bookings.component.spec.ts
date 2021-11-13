import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrentDayBookingsComponent } from './current-day-bookings.component';

describe('CurrentDayBookingsComponent', () => {
  let component: CurrentDayBookingsComponent;
  let fixture: ComponentFixture<CurrentDayBookingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CurrentDayBookingsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CurrentDayBookingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
