import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css'],
})
export class AdminDashboardComponent implements OnInit {
  users: { id: number; name: string; email:string; role: string }[] = [];
  projects: { id: number; pname: string; pm: string }[] = [];
  selectedUserId: number | null = null;

  constructor(private dbAccessService: DbAccessService, private router: Router) {}

  ngOnInit() {
    this.loadUsers();
    this.loadProjects();
  }

  loadUsers() {
    this.dbAccessService.GetAllUsers().subscribe(
      (users) => {
        this.users = users;
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    );
  }

  loadProjects() {
    this.dbAccessService.GetAllProjects().subscribe(
      (projects) => {
        this.projects = projects;
      },
      (error) => {
        console.error('Error fetching projects:', error);
      }
    );
  }

  NavigateToAddNewUser() {
    this.router.navigate(['addnewuser']);
  }

  NavigateToAddNewProject() {
    this.router.navigate(['addnewproject']);
  }
}
