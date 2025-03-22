import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { DbAccessService } from '../db-access.service';
import { Task } from '../task';
import { CommentInfo } from '../comment-info'; // Import the CommentInfo interface

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent implements OnInit {
  tasks: Task[] = []; // Tasks assigned to the logged-in user
  loggedInUserId: string = ''; // Logged-in user's ID
  selectedTaskId: string = ''; // Task ID for managing comments

  constructor(private authService: AuthService, private dbService: DbAccessService) {}

  ngOnInit(): void {
    const user = this.authService.getLoggedInUser();
    if (user) {
      this.loggedInUserId = user.id;
      this.loadTasks();
    }
  }

 
  loadTasks() {
    this.dbService.getTasksForUser(this.loggedInUserId).subscribe(
      (tasks) => {
        this.tasks = tasks; 
      },
      (error) => {
        console.error('Error fetching tasks:', error);
      }
    );
  }
  
  


  addComment(taskId: string, commentText: string) {
    const newComment: CommentInfo = {
      id: new Date().getTime().toString(),
      taskId,
      commentBy: this.loggedInUserId,
      description: commentText
    };

    this.dbService.addCommentToTask(taskId, newComment).subscribe(
      () => this.loadTasks(), 
      (error) => console.error('Error adding comment:', error)
    );
  }


  reassignTask(taskId: string, newUserId: string) {
    this.dbService.reassignTask(taskId, newUserId).subscribe(
      () => this.loadTasks(), 
      (error) => console.error('Error reassigning task:', error)
    );
  }
}
