import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { TaskInfo } from '../task-info';
import { Router } from '@angular/router';
@Component({
  selector: 'app-list-tasks',
  templateUrl: './list-tasks.component.html',
  styleUrls: ['./list-tasks.component.css']
})
export class ListTasksComponent implements OnInit {
  tasks: TaskInfo[] = [];

  constructor(private srv: DbAccessService) {}

  ngOnInit(): void {
    this.GetTasks();
  }

  GetTasks(): void {
    this.srv.GetTasks().subscribe({
      next: (tasks: TaskInfo[]) => {
        this.tasks = tasks;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
}