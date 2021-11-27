import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainWorkPlaceComponent } from './main-work-place.component';

describe('MainWorkPlaceComponent', () => {
  let component: MainWorkPlaceComponent;
  let fixture: ComponentFixture<MainWorkPlaceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MainWorkPlaceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MainWorkPlaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
