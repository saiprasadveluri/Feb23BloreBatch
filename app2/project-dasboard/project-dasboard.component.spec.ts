import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectDasboardComponent } from './project-dasboard.component';

describe('ProjectDasboardComponent', () => {
  let component: ProjectDasboardComponent;
  let fixture: ComponentFixture<ProjectDasboardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProjectDasboardComponent]
    });
    fixture = TestBed.createComponent(ProjectDasboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
