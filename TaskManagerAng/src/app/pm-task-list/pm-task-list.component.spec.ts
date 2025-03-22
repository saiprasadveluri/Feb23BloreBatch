import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmTaskListComponent } from './pm-task-list.component';

describe('PmTaskListComponent', () => {
  let component: PmTaskListComponent;
  let fixture: ComponentFixture<PmTaskListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PmTaskListComponent]
    });
    fixture = TestBed.createComponent(PmTaskListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
