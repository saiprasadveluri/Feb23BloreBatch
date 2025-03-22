import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { ProjectInfo } from '../project-info';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  projects: ProjectInfo[] = [];
  users: UserInfo[] = [];

  constructor(private router: Router, private srv: DbAccessService) {}

  ngOnInit(): void {
    this.loadProjects();
    this.loadUsers();
  }

  loadProjects(): void {
    this.srv.GetProjects().subscribe({
      next: (projects: ProjectInfo[]) => {
        this.projects = projects;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  loadUsers(): void {
    this.srv.GetAllUsers().subscribe({
      next: (users: UserInfo[]) => {
        this.users = users;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  deleteUser(userId: string): void {
    this.srv.DeleteUser(userId).subscribe({
      next: () => {
        this.loadUsers();
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
 

  NavigateToAddNewUser() {
    this.router.navigate(['addnewuser']);
  }

  NavigateToAddNewProject() {
    this.router.navigate(['addnewproject']);
  }

  logout(): void {
    sessionStorage.clear();
    this.router.navigate(['/login']);
  }
}
