import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { ProjectInfo } from '../project-info';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-add-new-project',
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})
export class AddNewProjectComponent implements OnInit {
  frmGroup: FormGroup;
  users: UserInfo[] = [];
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private srv: DbAccessService, private router: Router) {
    this.frmGroup = this.fb.group({
      Pname: ['', Validators.required],
      PM: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.srv.GetAllUsers().subscribe({
      next: (users) => {
        this.users = users.filter((user: UserInfo) => user.role === 'PM');
      },
      error: (err) => {
        console.error('Failed to fetch users', err);
        this.errorMessage = 'Failed to fetch users';
      }
    });
  }

  OnAddClick(): void {
    if (this.frmGroup.valid) {
      const newProject: ProjectInfo = {
        id: '',
        Pname: this.frmGroup.value.Pname,
        PM: this.frmGroup.value.PM
      };
      this.srv.AddNewProject(newProject).subscribe({
        next: () => {
          this.router.navigate(['admindashboard']);
        },
        error: (err) => {
          console.error('Failed to add project', err);
          this.errorMessage = 'Failed to add project';
        }
      });
    }
  }
}