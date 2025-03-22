import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNewCommentComponent } from './add-new-comment.component';

describe('AddNewCommentComponent', () => {
  let component: AddNewCommentComponent;
  let fixture: ComponentFixture<AddNewCommentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddNewCommentComponent]
    });
    fixture = TestBed.createComponent(AddNewCommentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
