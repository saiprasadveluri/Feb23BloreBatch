import { Component, Input } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { Comment } from '../comment';

@Component({
  selector: 'app-add-new-comment',
  templateUrl: './add-new-comment.component.html',
  styleUrls: ['./add-new-comment.component.css']
})
export class AddNewCommentComponent {
  @Input() taskId!: string;
  comment: Comment = { id: '', taskid: '', commentby: '', description: '' };

  constructor(private dbAccessService: DbAccessService) {}

  onSubmit() {
    this.comment.taskid = this.taskId;
    this.comment.commentby = 'currentUserId'; 
    this.dbAccessService.addComment(this.comment).subscribe();
  }
}