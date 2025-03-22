import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewOwnProjectsComponent } from './view-own-projects.component';

describe('ViewOwnProjectsComponent', () => {
  let component: ViewOwnProjectsComponent;
  let fixture: ComponentFixture<ViewOwnProjectsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViewOwnProjectsComponent]
    });
    fixture = TestBed.createComponent(ViewOwnProjectsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
