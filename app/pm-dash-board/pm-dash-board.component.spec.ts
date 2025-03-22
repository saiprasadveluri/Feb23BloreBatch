import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmDashBoardComponent } from './pm-dash-board.component';

describe('PmDashBoardComponent', () => {
  let component: PmDashBoardComponent;
  let fixture: ComponentFixture<PmDashBoardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PmDashBoardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PmDashBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
