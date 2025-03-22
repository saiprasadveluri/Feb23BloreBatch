import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddProjectMembersComponent } from './add-project-members.component';

describe('AddProjectMembersComponent', () => {
  let component: AddProjectMembersComponent;
  let fixture: ComponentFixture<AddProjectMembersComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddProjectMembersComponent]
    });
    fixture = TestBed.createComponent(AddProjectMembersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
