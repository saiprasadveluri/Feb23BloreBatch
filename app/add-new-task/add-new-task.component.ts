import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { Task } from '../task';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-add-new-task',
  templateUrl: './add-new-task.component.html',
  styleUrls: ['./add-new-task.component.css']
})
export class AddNewTaskComponent implements OnInit {
  frmGroup: FormGroup;
  users: UserInfo[] = [];
  projId: string = '';
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private srv: DbAccessService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.frmGroup = this.fb.group({
      Title: ['', Validators.required],
      description: ['', Validators.required],
      tasktype: ['', Validators.required],
      assignedto: ['', Validators.required],
      status: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.projId = this.route.snapshot.paramMap.get('projectId') || '';
    this.loadUsers();
  }

  loadUsers(): void {
    this.srv.GetAllUsers().subscribe({
      next: (users) => {
        this.users = users.filter((user: UserInfo) => user.role === 'DEVELOPER' || user.role === 'QA');
      },
      error: (err) => {
        console.error('Failed to fetch users', err);
        this.errorMessage = 'Failed to fetch users';
      }
    });
  }

  OnAddClick(): void {
    if (this.frmGroup.valid) {
      const newTask: Task = {
        id: '',
        projId: this.projId,
        Title: this.frmGroup.value.Title,
        description: this.frmGroup.value.description,
        tasktype: this.frmGroup.value.tasktype,
        assignedto: this.frmGroup.value.assignedto,
        status: this.frmGroup.value.status
      };
      this.srv.AddTask(newTask).subscribe({
        next: () => {
          this.router.navigate(['pmdashboard']);
        },
        error: (err) => {
          console.error('Failed to add task', err);
          this.errorMessage = 'Failed to add task';
        }
      });
    }
  }
}