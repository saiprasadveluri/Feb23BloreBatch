import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberUpdateTaskComponent } from './member-update-task.component';

describe('MemberUpdateTaskComponent', () => {
  let component: MemberUpdateTaskComponent;
  let fixture: ComponentFixture<MemberUpdateTaskComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MemberUpdateTaskComponent]
    });
    fixture = TestBed.createComponent(MemberUpdateTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
