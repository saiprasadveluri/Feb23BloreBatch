import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignMembersComponent } from './assign-members.component';

describe('AssignMembersComponent', () => {
  let component: AssignMembersComponent;
  let fixture: ComponentFixture<AssignMembersComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AssignMembersComponent]
    });
    fixture = TestBed.createComponent(AssignMembersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
