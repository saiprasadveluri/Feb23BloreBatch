import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReassigntaskComponent } from './reassigntask.component';

describe('ReassigntaskComponent', () => {
  let component: ReassigntaskComponent;
  let fixture: ComponentFixture<ReassigntaskComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ReassigntaskComponent]
    });
    fixture = TestBed.createComponent(ReassigntaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
