import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberCommentListComponent } from './member-comment-list.component';

describe('MemberCommentListComponent', () => {
  let component: MemberCommentListComponent;
  let fixture: ComponentFixture<MemberCommentListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MemberCommentListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MemberCommentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
