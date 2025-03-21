import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';
import { TaskInfo } from '../task-info';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.css']
})
export class CreateTaskComponent implements OnInit {
  frmGroup: FormGroup;
  members: UserInfo[] = [];
  projid = '1'; // Replace with the selected project's ID

  constructor(private fb: FormBuilder, private dbService: DbAccessService) {
    // Initialize the form group with validation
    this.frmGroup = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      tasktype: ['', Validators.required],
      assignedto: ['', Validators.required]
    });
  }

  ngOnInit() {
    // Fetch members assigned to the project
    this.dbService.GetMembersByProject(this.projid).subscribe(
      (data: UserInfo[]) => {
        this.members = data;
      },
      (error) => {
        console.error('Error fetching project members:', error);
        alert('Failed to load project members. Please try again later.');
      }
    );
  }

  OnCreateTask() {
    if (this.frmGroup.invalid) {
      alert('Please fill in all required fields.');
      return;
    }

    const task: Omit<TaskInfo, 'id'> = {
      projid: this.projid,
      title: this.frmGroup.controls['title'].value,
      description: this.frmGroup.controls['description'].value,
      tasktype: this.frmGroup.controls['tasktype'].value,
      assignedto: this.frmGroup.controls['assignedto'].value,
      status: 'ACTIVE'
    };

    // Call the service to create the task
    this.dbService.CreateTask(task).subscribe(
      () => {
        alert('Task created successfully!');
        this.frmGroup.reset(); // Reset the form after successful creation
      },
      (error) => {
        console.error('Error creating task:', error);
        alert('Failed to create task. Please try again later.');
      }
    );
  }
}