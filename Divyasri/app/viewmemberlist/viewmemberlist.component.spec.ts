import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewmemberlistComponent } from './viewmemberlist.component';

describe('ViewmemberlistComponent', () => {
  let component: ViewmemberlistComponent;
  let fixture: ComponentFixture<ViewmemberlistComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViewmemberlistComponent]
    });
    fixture = TestBed.createComponent(ViewmemberlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
