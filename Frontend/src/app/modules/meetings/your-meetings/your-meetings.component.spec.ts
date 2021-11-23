import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YourMeetingsComponent } from './your-meetings.component';

describe('YourMeetingsComponent', () => {
  let component: YourMeetingsComponent;
  let fixture: ComponentFixture<YourMeetingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YourMeetingsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(YourMeetingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
