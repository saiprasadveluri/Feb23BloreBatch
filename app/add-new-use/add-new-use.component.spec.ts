import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNewUseComponent } from './add-new-use.component';

describe('AddNewUseComponent', () => {
  let component: AddNewUseComponent;
  let fixture: ComponentFixture<AddNewUseComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddNewUseComponent]
    });
    fixture = TestBed.createComponent(AddNewUseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
