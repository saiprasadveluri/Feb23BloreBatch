import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QaPlaceComponent } from './qa-place.component';

describe('QaPlaceComponent', () => {
  let component: QaPlaceComponent;
  let fixture: ComponentFixture<QaPlaceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [QaPlaceComponent]
    });
    fixture = TestBed.createComponent(QaPlaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
