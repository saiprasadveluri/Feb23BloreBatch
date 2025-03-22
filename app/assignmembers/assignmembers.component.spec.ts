import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignmembersComponent } from './assignmembers.component';

describe('AssignmembersComponent', () => {
  let component: AssignmembersComponent;
  let fixture: ComponentFixture<AssignmembersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AssignmembersComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssignmembersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
