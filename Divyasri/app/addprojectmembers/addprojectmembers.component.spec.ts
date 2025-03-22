import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddprojectmembersComponent } from './addprojectmembers.component';

describe('AddprojectmembersComponent', () => {
  let component: AddprojectmembersComponent;
  let fixture: ComponentFixture<AddprojectmembersComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddprojectmembersComponent]
    });
    fixture = TestBed.createComponent(AddprojectmembersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
