import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { TaskInfo } from '../task-info';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.css']
})
export class AddTaskComponent {
  frmGroup: FormGroup;

  constructor(private fb: FormBuilder, private srv: DbAccessService, private router: Router) {
    this.frmGroup = this.fb.group({
      taskName: ['', [Validators.required]],
      assigneeId: ['', [Validators.required]]
    });
  }

  onAddTaskClick() {
    const taskName = this.frmGroup.controls['taskName'].value;
    const assigneeId = this.frmGroup.controls['assigneeId'].value;
    const projectId = sessionStorage.getItem('selectedProjectId'); // Assuming projectId is stored in sessionStorage

    if (projectId) {
      const newTask: TaskInfo = {
        id: '', // Assign a proper id value here
        name: taskName,
        projectId: projectId,
        assigneeId: assigneeId,
        status: 'ACTIVE'
      };

      this.srv.createTask(newTask).subscribe({
        next: (res: any) => {
          console.log('Task created successfully');
          this.router.navigate(['pmdashboard']); // Navigate back to PM Dashboard
        },
        error: (error: any) => {
          console.error('Error creating task', error);
        }
      });
    }
  }
}
