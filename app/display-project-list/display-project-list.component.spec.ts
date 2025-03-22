import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayProjectListComponent } from './display-project-list.component';

describe('DisplayProjectListComponent', () => {
  let component: DisplayProjectListComponent;
  let fixture: ComponentFixture<DisplayProjectListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DisplayProjectListComponent]
    });
    fixture = TestBed.createComponent(DisplayProjectListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
