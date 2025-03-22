import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdtaskComponent } from './adtask.component';

describe('AdtaskComponent', () => {
  let component: AdtaskComponent;
  let fixture: ComponentFixture<AdtaskComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdtaskComponent]
    });
    fixture = TestBed.createComponent(AdtaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
