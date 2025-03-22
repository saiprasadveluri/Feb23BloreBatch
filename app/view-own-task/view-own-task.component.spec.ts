import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewOwnTaskComponent } from './view-own-task.component';

describe('ViewOwnTaskComponent', () => {
  let component: ViewOwnTaskComponent;
  let fixture: ComponentFixture<ViewOwnTaskComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViewOwnTaskComponent]
    });
    fixture = TestBed.createComponent(ViewOwnTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
