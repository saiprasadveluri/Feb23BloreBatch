import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { Task } from '../task';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent implements OnInit {
  tasks: Task[] = [];
  showCommentForm = false;
  selectedTaskId: string = '';
  isDeveloper = false;
  isQA = false;

  constructor(private dbAccessService: DbAccessService, private authService: AuthService) {}

  ngOnInit() {
    const currentUser = this.authService.getCurrentUser();
    console.log('Current User:', currentUser); 
    if (currentUser) {
      this.isDeveloper = currentUser.role === 'DEVELOPER';
      this.isQA = currentUser.role === 'QA';
      if (currentUser.id) {
        this.loadTasks(currentUser.id);
      }
    }
  }

  loadTasks(userId: string) {
    this.dbAccessService.getTasksByUser(userId).subscribe(tasks => {
      console.log('Fetched Tasks:', tasks); 
      this.tasks = tasks.filter(task => task.assignedto === userId);
      console.log('Filtered Tasks:', this.tasks); 
    });
  }

  addComment(taskId: string) {
    this.selectedTaskId = taskId;
    this.showCommentForm = true;
  }

  resolveTask(taskId: string) {
    this.updateStatus(taskId, 'Resolved');
    this.assignTask(taskId, 'QA');
  }

  closeTask(taskId: string) {
    this.updateStatus(taskId, 'Closed');
  }

  reopenTask(taskId: string) {
    this.updateStatus(taskId, 'Reopened');
    this.assignTask(taskId, 'DEVELOPER');
  }

  updateStatus(taskId: string, status: string) {
    this.dbAccessService.updateTaskStatus(taskId, status).subscribe();
  }

  assignTask(taskId: string, role: string) {
    this.dbAccessService.assignTask(taskId, role).subscribe();
  }
}