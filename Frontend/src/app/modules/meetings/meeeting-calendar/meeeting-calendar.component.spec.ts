import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeeetingCalendarComponent } from './meeeting-calendar.component';

describe('MeeetingCalendarComponent', () => {
  let component: MeeetingCalendarComponent;
  let fixture: ComponentFixture<MeeetingCalendarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MeeetingCalendarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MeeetingCalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
