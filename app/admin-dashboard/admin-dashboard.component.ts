import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { Project } from '../project';
import { Userinfo } from '../userinfo';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  projects: Project[] = [];
  users: Userinfo[] = [];

  constructor(private route: Router, private dbAccessService: DbAccessService) {}

  ngOnInit(): void {
    this.getAllProjects();
    this.getAllUsers(); 
  }

  getAllProjects(): void {
    this.dbAccessService.GetAllProjects().subscribe((projects: Project[]) => {
      this.projects = projects;
    });
  }

  getAllUsers(): void {
    this.dbAccessService.GetAllUsers().subscribe((users: Userinfo[]) => {
      this.users = users;
    });
  }

  deleteUser(userId: string): void {
    this.dbAccessService.DeleteUser(userId).subscribe({
      next: () => {
        console.log('User deleted successfully');
        this.getAllUsers(); 
      },
      error: (err) => {
        console.error('Error deleting user:', err);
      }
    });
  }

  NavigateToAddNewUser() {
    this.route.navigate(['/addnewuser']);
  }

  NavigateToAddNewProject() {
    this.route.navigate(['/addnewproject']);
  }
}