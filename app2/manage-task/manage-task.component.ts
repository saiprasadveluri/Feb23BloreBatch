import { Component, OnInit } from '@angular/core';
import { UserInfo } from '../user-info';
import { DBAccessService } from '../dbaccess.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Task } from '../task';

@Component({
  selector: 'app-manage-task',
  templateUrl: './manage-task.component.html',
  styleUrls: ['./manage-task.component.css']
})
export class ManageTaskComponent implements OnInit {
  projectId: string = '';
  tasks: Task[] = [];
  newTask: string = '';
  frmGroup: any;

  constructor(private route: Router, private srv: DBAccessService) {}

  ngOnInit(): void {
    this.projectId = this.route.snapshot.paramMap.get('id') || '';
    this.loadTasks();
  }

  loadTasks(): void {
    this.srv.GetProjectTasks(this.projectId).subscribe({
      next: (data) => {
        this.tasks = data as Task[]; // Successfully fetch tasks
      },
      error: (err) => {
        console.error('Error fetching tasks:', err);
      }
    });
  }

  OnAddTaskClick() {
    var projId = this.frmGroup.controls['projId'].value;
    var title = this.frmGroup.controls['title'].value;
    var description = this.frmGroup.controls['description'].value;
    var tasktype = this.frmGroup.controls['tasktype'].value;
    var assignedto = this.frmGroup.controls['assignedto'].value;
    var status = this.frmGroup.controls['status'].value;
  
    var newTask = {
      id: '', // This will be auto-generated in db.json or backend
      projId: projId,
      title: title,
      description: description,
      tasktype: tasktype,
      assignedto: assignedto,
      status: status
    };
  
    this.srv.AddNewTask(newTask).subscribe({
      next: (res) => {
        console.log("New task added: " + res.id);
        this.router.navigate(['taskdashboard']); // Navigate to the desired page after task creation
      },
      error: (err) => {
        console.error('Error adding task:', err);
      }
    });
  }
  

  removeTask(taskId: string): void {
    this.srv.RemoveTaskFromProject(this.projectId, taskId).subscribe({
      next: () => {
        this.loadTasks(); // Refresh list
      },
      error: (err) => {
        console.error('Error removing task:', err);
      }
    });
  }
}


