import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { ActivatedRoute } from '@angular/router';
import { Userinfo } from '../userinfo';
import { Task } from '../task';
import { ProjectMembers } from '../project-members';

@Component({
  selector: 'app-task-management',
  templateUrl: './task-management.component.html',
  styleUrls: ['./task-management.component.css']
})
export class TaskManagementComponent implements OnInit {
  frmGroup: FormGroup;
  projectId: string | null = null;
  members: Userinfo[] = [];
  projectMembers: ProjectMembers[] = [];

  constructor(private fb: FormBuilder, private srv: DbAccessService, private route: ActivatedRoute) {
    this.frmGroup = this.fb.group({
      title: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required]),
      assignedTo: new FormControl('', [Validators.required]),
      taskType: new FormControl('', [Validators.required]),
      status: new FormControl('Active', [Validators.required])
    });
  }

  ngOnInit(): void {
    this.projectId = this.route.snapshot.paramMap.get('id');
    if (this.projectId) {
      this.srv.GetProjectMembers(this.projectId).subscribe({
        next: (projectMembers: ProjectMembers[]) => {
          this.projectMembers = projectMembers;
          this.srv.GetAllUsers().subscribe({
            next: (users: Userinfo[]) => {
              this.members = users.filter(user => 
                this.projectMembers.some(pm => pm.memid === user.id) && user.role !== 'ADMIN'
              );
            },
            error: (err: any) => {
              console.error('Error fetching users:', err);
            }
          });
        },
        error: (err: any) => {
          console.error('Error fetching project members:', err);
        }
      });
    }
  }

  onAddTask(): void {
    if (this.projectId) {
      var newTask: Task = {
        projid: this.projectId,
        title: this.frmGroup.controls['title'].value,
        description: this.frmGroup.controls['description'].value,
        assignedto: this.frmGroup.controls['assignedTo'].value,
        tasktype: this.frmGroup.controls['taskType'].value,
        status: this.frmGroup.controls['status'].value
      };
      this.srv.AddNewTask(newTask).subscribe({
        next: (res: Task) => {
          console.log('New Task Added:', res.id);
        },
        error: (err: any) => {
          console.error('Error adding task:', err);
        }
      });
    }
  }
}