import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { ProjectInfo } from '../project-info';
import { Router } from '@angular/router';
import { UserInfo } from '../user-info';
import { ProjectManager } from '../projectmanager';
import { Task } from '../task';

@Component({
  selector: 'app-pm-dashboard',
  templateUrl: './pm-dashboard.component.html',
  styleUrls: ['./pm-dashboard.component.css']
})
export class PmDashboardComponent implements OnInit {
  projects: ProjectInfo[] = [];
  loggedInUser: UserInfo;
  projId: string = '';
  members: ProjectManager[] = [];
  users: UserInfo[] = [];
  tasks: Task[] = [];
  errorMessage: string = '';

  constructor(private srv: DbAccessService, private router: Router) {
    this.loggedInUser = JSON.parse(sessionStorage.getItem('LoggedinUserData') as string);
  }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadProjects(): void {
    this.srv.GetAllProjects().subscribe({
      next: (projects) => {
        this.projects = projects.filter((project: ProjectInfo) => project.PM === this.loggedInUser.id);
        this.projects.forEach(project => {
          project.PM = this.getUserName(project.PM);
        });
      },
      error: (err) => {
        console.error('Failed to fetch projects', err);
      }
    });
  }

  viewProjectMembers(projectId: string): void {
    this.projId = projectId;
    this.loadMembers();
    this.loadTasks();
  }

  loadMembers(): void {
    this.srv.GetProjectMembers(this.projId).subscribe({
      next: (members) => {
        this.members = members;
      },
      error: (err) => {
        console.error('Failed to fetch project members', err);
        this.errorMessage = 'Failed to fetch project members';
      }
    });
  }

  loadTasks(): void {
    this.srv.GetTasks(this.projId).subscribe({
      next: (tasks) => {
        this.tasks = tasks;
      },
      error: (err) => {
        console.error('Failed to fetch tasks', err);
        this.errorMessage = 'Failed to fetch tasks';
      }
    });
  }

  loadUsers(): void {
    this.srv.GetAllUsers().subscribe({
      next: (users) => {
        this.users = users.filter((user: UserInfo) => user.role === 'DEVELOPER' || user.role === 'QA');
        this.loadProjects(); 
      },
      error: (err) => {
        console.error('Failed to fetch users', err);
        this.errorMessage = 'Failed to fetch users';
      }
    });
  }

  assignMember(userId: string): void {
    const newMember: ProjectManager = {
      id: '',
      projId: this.projId,
      memid: userId
    };
    this.srv.AddProjectMember(newMember).subscribe({
      next: () => {
        this.loadMembers();
      },
      error: (err) => {
        console.error('Failed to assign member', err);
        this.errorMessage = 'Failed to assign member';
      }
    });
  }

  removeMember(memberId: string): void {
    console.log(`Removing member with ID: ${memberId}`);
    this.srv.RemoveProjectMember(memberId).subscribe({
      next: () => {
        console.log('Member removed successfully');
        this.loadMembers();
      },
      error: (err) => {
        console.error('Failed to remove member', err);
        this.errorMessage = 'Failed to remove member';
      }
    });
  }

  manageTasks(projectId: string): void {
    this.router.navigate(['add-new-task', projectId]);
  }

  getUserName(userId: string): string {
    const user = this.users.find(user => user.id === userId);
    return user ? user.name : 'No member assigned';
  }

  getUserRole(userId: string): string {
    const user = this.users.find(user => user.id === userId);
    return user ? user.role : 'No member assigned';
  }
}