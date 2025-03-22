import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PmprojectListComponent } from './pmproject-list.component';

describe('PmprojectListComponent', () => {
  let component: PmprojectListComponent;
  let fixture: ComponentFixture<PmprojectListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PmprojectListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PmprojectListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
