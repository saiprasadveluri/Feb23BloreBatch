import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QaDashboardComponent } from './qa-dashboard.component';

describe('QaDashboardComponent', () => {
  let component: QaDashboardComponent;
  let fixture: ComponentFixture<QaDashboardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [QaDashboardComponent]
    });
    fixture = TestBed.createComponent(QaDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
