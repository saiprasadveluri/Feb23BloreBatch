import { Component } from '@angular/core';
import { Router } from '@angular/router';

interface Task {
  id: number;
  title: string;
  status: 'Open' | 'Development' | 'QA' | 'Closed';
  comments: string[];
}

@Component({
  selector: 'app-member-dashboard',
  templateUrl: './member-dashboard.component.html',
  styleUrls: ['./member-dashboard.component.css']
})
export class MemberDashboardComponent {
  tasks: Task[] = [
    { id: 1, title: 'Task 1', status: 'Open', comments: [] },
    { id: 2, title: 'Task 2', status: 'Development', comments: [] },
    { id: 3, title: 'Task 3', status: 'QA', comments: [] },
    { id: 4, title: 'Task 4', status: 'Closed', comments: [] }
  ];

  constructor(private router: Router) {}

  navigateToCloseTask() {
    this.router.navigate(['close-task']);
  }

  navigateToAddComment() {
    this.router.navigate(['add-comment']);
  }

  navigateToReopenTask() {
    this.router.navigate(['reopen-task']);
  }

  viewAssignedTasks() {
    return this.tasks;
  }

  addComment(taskId: number, comment: string) {
    const task = this.tasks.find(t => t.id === taskId);
    if (task) {
      task.comments.push(comment);
    }
  }

  changeTaskStatus(taskId: number, status: 'Open' | 'Development' | 'QA' | 'Closed') {
    const task = this.tasks.find(t => t.id === taskId);
    if (task) {
      task.status = status;
    }
  }

  workOnTask(taskId: number) {
    const task = this.tasks.find(t => t.id === taskId);
    if (task && task.status === 'Open') {
      task.status = 'Development';
    }
  }

  qaTask(taskId: number) {
    const task = this.tasks.find(t => t.id === taskId);
    if (task && task.status === 'Development') {
      task.status = 'QA';
    }
  }
}
