import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectMemComponent } from './project-mem.component';

describe('ProjectMemComponent', () => {
  let component: ProjectMemComponent;
  let fixture: ComponentFixture<ProjectMemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProjectMemComponent]
    });
    fixture = TestBed.createComponent(ProjectMemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
