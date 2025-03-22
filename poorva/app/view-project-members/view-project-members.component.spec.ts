import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewProjectMembersComponent } from './view-project-members.component';

describe('ViewProjectMembersComponent', () => {
  let component: ViewProjectMembersComponent;
  let fixture: ComponentFixture<ViewProjectMembersComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViewProjectMembersComponent]
    });
    fixture = TestBed.createComponent(ViewProjectMembersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
