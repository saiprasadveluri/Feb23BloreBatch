import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddprojectlistComponent } from './addprojectlist.component';

describe('AddprojectlistComponent', () => {
  let component: AddprojectlistComponent;
  let fixture: ComponentFixture<AddprojectlistComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddprojectlistComponent]
    });
    fixture = TestBed.createComponent(AddprojectlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
