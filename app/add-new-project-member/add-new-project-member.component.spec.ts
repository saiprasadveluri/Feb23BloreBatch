import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNewProjectMemberComponent } from './add-new-project-member.component';

describe('AddNewProjectMemberComponent', () => {
  let component: AddNewProjectMemberComponent;
  let fixture: ComponentFixture<AddNewProjectMemberComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddNewProjectMemberComponent]
    });
    fixture = TestBed.createComponent(AddNewProjectMemberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
