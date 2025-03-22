import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { TaskInfo } from '../task-info';
import { ProjectInfo } from '../project-info';
import { UserInfo } from '../user-info';
import { ProjectmemberInfo } from '../projectmember-info';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.css']
})
export class CreateTaskComponent implements OnInit {
  frmGroup: FormGroup;
  projects: ProjectInfo[] = [];
  Users: UserInfo[] = [];
  projectMembers: UserInfo[] = [];
  pmId: string = '';

  constructor(private fb: FormBuilder, private srv: DbAccessService, private router: Router) {
    this.frmGroup = this.fb.group({
      projid: new FormControl('', [Validators.required]),
      title: new FormControl('', [Validators.required]),
      tasktype: new FormControl('', [Validators.required]),
      Assignedto: new FormControl('', [Validators.required]),
      status: new FormControl('', [Validators.required])
    });
  }

  ngOnInit(): void {
    const loggedInUserData = sessionStorage.getItem('LoggedInUserData');
    if (loggedInUserData) {
      const currentUser: UserInfo = JSON.parse(loggedInUserData);
      if (currentUser.role.toUpperCase() === 'PM') {
        this.pmId = currentUser.id!;
        this.loadProjects();
        this.loadUsers();
        this.frmGroup.controls['projid'].valueChanges.subscribe(projid => {
          this.loadProjectMembers(projid);
        });
      }
    }
  }

  loadProjects(): void {
    this.srv.GetProjects().subscribe({
      next: (projects: ProjectInfo[]) => {
        this.projects = projects.filter(project => project.pm === this.pmId);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  loadUsers(): void {
    this.srv.GetAllUsers().subscribe({
      next: (users: UserInfo[]) => {
        this.Users = users;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  loadProjectMembers(projid: string): void {
    this.srv.GetPProjectMembers(projid).subscribe({
      next: (projectMembers: ProjectmemberInfo[]) => {
        const memberIds = projectMembers.map(member => member.memid);
        this.projectMembers = this.Users.filter(user => memberIds.includes(user.id!) && (user.role.toUpperCase() === 'DEVELOPER' || user.role.toUpperCase() === 'QA'));
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  OnCreateClick() {
    const newTask: TaskInfo = {
      projid: this.frmGroup.controls['projid'].value,
      title: this.frmGroup.controls['title'].value,
      tasktype: this.frmGroup.controls['tasktype'].value,
      Assignedto: this.frmGroup.controls['Assignedto'].value,
      status: this.frmGroup.controls['status'].value
    };

    this.srv.AddNewTask(newTask).subscribe({
      next: (res) => {
        console.log("New Task Created" + res.id);
        this.router.navigate(['pmdashboard']);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  logout(): void {
    sessionStorage.clear();
    this.router.navigate(['/login']);
  }
}