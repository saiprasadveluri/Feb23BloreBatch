import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeveloperDashboardComponent } from './developer-dashboard.component';

describe('DeveloperDashboardComponent', () => {
  let component: DeveloperDashboardComponent;
  let fixture: ComponentFixture<DeveloperDashboardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeveloperDashboardComponent]
    });
    fixture = TestBed.createComponent(DeveloperDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
