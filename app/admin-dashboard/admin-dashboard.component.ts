import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DBAccessService } from '../dbaccess.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  users: any[] = []; // Array to store users
  projects: any[] = []; // Array to store projects

  constructor(private router: Router, private adminService: DBAccessService) {}

  ngOnInit(): void {
    this.loadUsers(); // Fetch users when the component initializes
    this.loadProjects(); // Fetch projects when the component initializes
  }

  // Fetch users from db.json
  loadUsers(): void {
    this.adminService.GetAllUsers().subscribe(data => {
      this.users = data;
    });
  }

  // Fetch projects from db.json
  loadProjects(): void {
    this.adminService.GetAllProjects().subscribe(data => {
      this.projects = data;
    });
  }

  // Delete a user by ID
  deleteUser(userId: string): void {
    if (confirm('Are you sure you want to delete this user?')) {
      this.adminService.deleteUser(userId).subscribe(() => {
        alert('User deleted successfully');
        this.loadUsers(); // Reload the user list after deletion
      });
    }
  }

  // Delete a project by ID
  deleteProject(projectId: string): void {
    if (confirm('Are you sure you want to delete this project?')) {
      this.adminService.deleteProject(projectId).subscribe(() => {
        alert('Project deleted successfully');
        this.loadProjects(); // Reload the project list after deletion
      });
    }
  }

  // Navigate to Add New User page
  NavigateToAddNewUser(): void {
    this.router.navigate(['addnewuser']);
  }

  // Navigate to Add New Project page
  NavigateToAddNewProject(): void {
    this.router.navigate(['addnewproject']);
  }
}