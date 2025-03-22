import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';


@Component({
  selector: 'app-developer-dashboard',
  templateUrl: './developer-dashboard.component.html',
  styleUrls: ['./developer-dashboard.component.css']
})
export class DeveloperDashboardComponent implements OnInit {
  tasks: TaskInfo[] = [];

  constructor(private dbAccessService: DbAccessService) { }

  ngOnInit(): void {
    this.fetchTasks();
  }

  fetchTasks(): void {
    this.dbAccessService.getTasks().subscribe(
      (tasks: TaskInfo[]) => this.tasks = tasks,
      error => console.error('Error fetching tasks:', error)
    );
  }

  updateTaskStatus(taskId: number, newStatus: string): void {
    this.dbAccessService.updateTaskStatus(taskId, newStatus).subscribe(
      () => {
        const task = this.tasks.find(t => t.id === taskId);
        if (task) {
          task.status = newStatus;
        }
      },
      error => console.error('Error updating task status:', error)
    );
  }

  addComment(taskId: number): void {
    // Logic to add a comment to the task (this could open a modal or redirect to a comment page)
    console.log(`Add comment to task with ID: ${taskId}`);
  }
}