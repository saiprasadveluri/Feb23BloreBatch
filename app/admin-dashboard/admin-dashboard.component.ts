import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user.info';
import { ProjectInfo } from '../project.info';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {
  users:UserInfo[]=[];
  projects:ProjectInfo[]=[];
  constructor(private router:Router,private srv:DbAccessService){


  }

  ngOnInit(): void {
    this.listAllUsers(); // Fetch users when the component initializes
    this.listAllProjects();
  }

  listAllUsers(): void {
    this.srv.GetAllUsers().subscribe({
      next: (data) => {
        this.users = data; // Assign the fetched user data to the array
      },
      error: (err) => {
        console.error('Error fetching users:', err);
      }
    });
  }


  listAllProjects():void{
    this.srv.GetAllProjects().subscribe({
      next: (data) => {
        this.projects = data; // Assign the fetched project data to the array
      },
      error: (err) => {
        console.error('Error fetching projects:', err);
      }
    });
  }

  deleteUser(userId: number): void {
    this.srv.DeleteUser(userId).subscribe({
      next: () => {
        console.log(`User with ID ${userId} deleted.`);
        this.listAllUsers(); // Refresh the user list after deletion
      },
      error: (err) => {
        console.error('Error deleting user:', err);
      }
    });
  }

  NavigateToAddUser(){
    this.router.navigate(['addnewuser']);
  }


NavigateToAddProject(){
  this.router.navigate(['addnewproject']);
}




}
