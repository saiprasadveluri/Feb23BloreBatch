import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberdashboardComponent } from './memberdashboard.component';

describe('MemberdashboardComponent', () => {
  let component: MemberdashboardComponent;
  let fixture: ComponentFixture<MemberdashboardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MemberdashboardComponent]
    });
    fixture = TestBed.createComponent(MemberdashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
