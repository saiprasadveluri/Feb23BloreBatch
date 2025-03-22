import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddProjectMemberComponent } from './add-project-member.component';

describe('AddProjectMemberComponent', () => {
  let component: AddProjectMemberComponent;
  let fixture: ComponentFixture<AddProjectMemberComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddProjectMemberComponent]
    });
    fixture = TestBed.createComponent(AddProjectMemberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
