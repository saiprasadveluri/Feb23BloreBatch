import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { TaskInfo } from '../task-info';
import { CommentInfo } from '../comment-info';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent implements OnInit {
  tasks: TaskInfo[] = [];
  selectedTaskId: string | null = null;
  comments: CommentInfo[] = [];

  constructor(private srv: DbAccessService) {}

  ngOnInit() {
    const loggedInUser = JSON.parse(sessionStorage.getItem('LoggedinUserData') || '{}');
    this.loadTasks(loggedInUser.id);
  }

  loadTasks(userId: string) {
    this.srv.getTasksByUser(userId).subscribe({
      next: (data: TaskInfo[]) => {
        this.tasks = data;
      },
      error: (error: any) => {
        console.error('Error loading tasks', error);
      }
    });
  }

  viewComments(taskId: string) {
    this.selectedTaskId = taskId;
    this.srv.getCommentsByTask(taskId).subscribe({
      next: (data: CommentInfo[]) => {
        this.comments = data;
      },
      error: (error: any) => {
        console.error('Error loading comments', error);
      }
    });
  }

  addComment(commentText: string) {
    if (this.selectedTaskId) {
      const newComment: CommentInfo = {
        id: '', // Assign a unique id here
        taskId: this.selectedTaskId as string,
        text: commentText,
        createdBy: JSON.parse(sessionStorage.getItem('LoggedinUserData') || '{}').id
      };
      this.srv.addComment(newComment).subscribe({
        next: (res: any) => {
          console.log('Comment added successfully');
          this.viewComments(this.selectedTaskId as string); // Refresh the comments
        },
        error: (error: any) => {
          console.error('Error adding comment', error);
        }
      });
    }
  }

  reassignTask(taskId: string, newAssigneeId: string) {
    this.srv.reassignTask(taskId, newAssigneeId).subscribe({
      next: (res: any) => {
        console.log('Task reassigned successfully');
        this.loadTasks(JSON.parse(sessionStorage.getItem('LoggedinUserData') || '{}').id); // Refresh the tasks
      },
      error: (error: any) => {
        console.error('Error reassigning task', error);
      }
    });
  }
}