import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';
import { ProjectInfo } from '../project-info';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  users: UserInfo[] = [];
  projects: ProjectInfo[] = [];

  constructor(private router: Router, private srv: DbAccessService) {}

  ngOnInit(): void {
    this.loadUsers();
    this.loadProjects();
  }

  loadUsers(): void {
    this.srv.GetAllUsers().subscribe({
      next: (users) => {
        this.users = users;
      },
      error: (err) => {
        console.error('Failed to fetch users', err);
      }
    });
  }

  loadProjects(): void {
    this.srv.GetAllProjects().subscribe({
      next: (projects) => {
        this.projects = projects;
      },
      error: (err) => {
        console.error('Failed to fetch projects', err);
      }
    });
  }

  deleteUser(userId: string): void {
    if (userId) {
      this.srv.DeleteUser(userId).subscribe({
        next: () => {
          this.loadUsers();
        },
        error: (err) => {
          console.error('Failed to delete user', err);
        }
      });
    }
  }

  deleteProject(projectId: string): void {
    if (projectId) {
      this.srv.DeleteProject(projectId).subscribe({
        next: () => {
          this.loadProjects();
        },
        error: (err) => {
          console.error('Failed to delete project', err);
        }
      });
    }
  }

  NavigateToAddNewUser() {
    this.router.navigate(['addnewuser']);
  }

  NavigateToAddNewProject() {
    this.router.navigate(['addnewproject']);
  }
}