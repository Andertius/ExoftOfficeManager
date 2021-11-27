import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainMeetingComponent } from './main-meeting.component';

describe('MainMeetingComponent', () => {
  let component: MainMeetingComponent;
  let fixture: ComponentFixture<MainMeetingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MainMeetingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MainMeetingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
