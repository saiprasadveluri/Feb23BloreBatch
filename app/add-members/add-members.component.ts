import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { ProjectInfo } from '../project-info';
import { UserInfo } from '../user-info';
import { ProjectmemberInfo } from '../projectmember-info';

@Component({
  selector: 'app-add-members',
  templateUrl: './add-members.component.html',
  styleUrls: ['./add-members.component.css']
})
export class AddMembersComponent implements OnInit {
  frmGroup: FormGroup;
  projects: ProjectInfo[] = [];
  users: UserInfo[] = [];
  pmId: string = '';

  constructor(private fb: FormBuilder, private srv: DbAccessService, private router: Router) {
    this.frmGroup = this.fb.group({
      projid: new FormControl('', [Validators.required]),
      memid: new FormControl('', [Validators.required])
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
        this.users = users.filter(user => user.role.toUpperCase() === 'DEVELOPER' || user.role.toUpperCase() === 'QA');
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  OnAddMemberClick() {
    const newMember: ProjectmemberInfo = {
      projid: this.frmGroup.controls['projid'].value,
      memid: this.frmGroup.controls['memid'].value
    };

    this.srv.AddProjectMember(newMember).subscribe({
      next: (res) => {
        console.log("Member Added to Project");
        this.router.navigate(['/pmdashboard']);
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