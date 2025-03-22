import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmProjectListComponent } from './pm-project-list.component';

describe('PmProjectListComponent', () => {
  let component: PmProjectListComponent;
  let fixture: ComponentFixture<PmProjectListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PmProjectListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PmProjectListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
