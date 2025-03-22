import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewtasklistComponent } from './viewtasklist.component';

describe('ViewtasklistComponent', () => {
  let component: ViewtasklistComponent;
  let fixture: ComponentFixture<ViewtasklistComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViewtasklistComponent]
    });
    fixture = TestBed.createComponent(ViewtasklistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
