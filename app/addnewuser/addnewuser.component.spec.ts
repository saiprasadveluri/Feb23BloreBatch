import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNewUserComponent } from './addnewuser.component';

describe('AddnewuserComponent', () => {
  let component: AddNewUserComponent;
  let fixture: ComponentFixture<AddNewUserComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddNewUserComponent]
    });
    fixture = TestBed.createComponent(AddNewUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
