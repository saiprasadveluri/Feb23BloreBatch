import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { TaskInfo } from '../task-info';

@Component({
  selector: 'app-view-tasks',
  templateUrl: './view-tasks.component.html',
  styleUrls: ['./view-tasks.component.css']
})
export class ViewTasksComponent implements OnInit {
  tasks: TaskInfo[] = [];
  projid = '1'; // Replace with the selected project's ID

  constructor(private dbService: DbAccessService) {}

  ngOnInit() {
    this.dbService.GetTasksByProject(this.projid).subscribe((data: TaskInfo[]) => {
      this.tasks = data;
    });
  }
}