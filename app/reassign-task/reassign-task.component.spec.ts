import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReassignTaskComponent } from './reassign-task.component';

describe('ReassignTaskComponent', () => {
  let component: ReassignTaskComponent;
  let fixture: ComponentFixture<ReassignTaskComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ReassignTaskComponent]
    });
    fixture = TestBed.createComponent(ReassignTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
