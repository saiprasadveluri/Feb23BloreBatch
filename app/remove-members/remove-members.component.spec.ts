import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoveMembersComponent } from './remove-members.component';

describe('RemoveMembersComponent', () => {
  let component: RemoveMembersComponent;
  let fixture: ComponentFixture<RemoveMembersComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RemoveMembersComponent]
    });
    fixture = TestBed.createComponent(RemoveMembersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
