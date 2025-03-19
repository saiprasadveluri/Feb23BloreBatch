import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddnewrestaurantComponent } from './addnewrestaurant.component';

describe('AddnewrestaurantComponent', () => {
  let component: AddnewrestaurantComponent;
  let fixture: ComponentFixture<AddnewrestaurantComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddnewrestaurantComponent]
    });
    fixture = TestBed.createComponent(AddnewrestaurantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
