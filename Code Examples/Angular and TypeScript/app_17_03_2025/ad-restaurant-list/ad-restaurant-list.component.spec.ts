import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdRestaurantListComponent } from './ad-restaurant-list.component';

describe('AdRestaurantListComponent', () => {
  let component: AdRestaurantListComponent;
  let fixture: ComponentFixture<AdRestaurantListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdRestaurantListComponent]
    });
    fixture = TestBed.createComponent(AdRestaurantListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
