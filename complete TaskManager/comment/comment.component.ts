import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { CommentInfo } from '../comment-info'; // Import CommentInfo interface

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
  @Input() taskId: string = '';
  @Output() commentAdded = new EventEmitter<void>(); // Notify parent component
  comments: CommentInfo[] = [];
  newCommentText: string = '';

  constructor(private dbService: DbAccessService) {}

  ngOnInit(): void {
    this.loadComments();
  }


  loadComments() {
    this.dbService.getCommentsForTask(this.taskId).subscribe(
      (comments) => (this.comments = comments),
      (error) => console.error('Error loading comments:', error)
    );
  }

  addComment() {
    const newComment: CommentInfo = {
      id: new Date().getTime().toString(),
      taskId: this.taskId,
      commentBy: 'currentUserId',
      description: this.newCommentText
    };

    this.dbService.addCommentToTask(this.taskId, newComment).subscribe(
      () => {
        this.newCommentText = '';
        this.commentAdded.emit(); // Notify parent component
        this.loadComments();
      },
      (error) => console.error('Error adding comment:', error)
    );
  }
}
