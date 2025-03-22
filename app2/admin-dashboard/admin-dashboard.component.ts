import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DBAccessService } from '../dbaccess.service';
import { UserInfo } from '../user-info';
import { ProjectInfo } from '../project-info';

@Component({
  selector: 'app-admin-dashboard',
  standalone:false,
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit{
  users: UserInfo[] = [];
  projects: ProjectInfo[] = [];
constructor(private router:Router,private srv:DBAccessService){

}
  ngOnInit(): void {
    this.loadUsers();
    this.loadProjects();
  }
  loadProjects() {
    this.srv.GetAllUsers().subscribe({
      next: (users) => {
        this.users = users; // Assign fetched users to the array
      },
      error: (err) => {
        console.error('Error fetching users:', err);
      }
    });
  }
  loadUsers() {
    this.srv.GetAllProjects().subscribe({
      next: (projects: ProjectInfo[]) => {
        this.projects = projects; // Assign fetched projects to the array
      },
      error: (err: any) => {
        console.error('Error fetching projects:', err);
      }
    });
  }
NavigateToAddNewUser(){
  this.router.navigate(['addnewuser'])
  
}
NavigateToAddNewProject(){
  this.router.navigate(['addnewproject'])
}

}
