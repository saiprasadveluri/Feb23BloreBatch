import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeveloperPlaceComponent } from './developer-place.component';

describe('DeveloperPlaceComponent', () => {
  let component: DeveloperPlaceComponent;
  let fixture: ComponentFixture<DeveloperPlaceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeveloperPlaceComponent]
    });
    fixture = TestBed.createComponent(DeveloperPlaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
