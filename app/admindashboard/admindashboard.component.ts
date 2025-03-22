import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TmaccessService } from '../tmaccess.service';

@Component({
  selector: 'app-admindashboard',
  templateUrl: './admindashboard.component.html',
  styleUrls: ['./admindashboard.component.css']
})
export class AdmindashboardComponent implements OnInit {
  projectinfo: any[] = []; // Array to hold all projects
  userinfo: any[] = []; // Array to hold all users

  constructor(private router: Router, private srv: TmaccessService) {}

  ngOnInit(): void {
    // Fetch all projects and store them in the projectinfo array
    this.srv.getallprojects().subscribe({
      next: (projects: any[]) => {
        this.projectinfo = projects; // Assign the fetched projects to the projectinfo array
      },
      error: (err) => {
        console.error('Error fetching projects:', err);
      }
    });

    // Fetch all users and store them in the userinfo array
    this.srv.getallusers().subscribe({
      next: (users: any[]) => {
        this.userinfo = users; // Assign the fetched users to the userinfo array
      },
      error: (err) => {
        console.error('Error fetching users:', err);
      }
    });
  }

  // Method to delete a user
  deleteUser(userId: number): void {
    this.srv.deleteUser(userId).subscribe({
      next: () => {
        this.userinfo = this.userinfo.filter(user => user.id !== userId); // Remove the deleted user from the array
        console.log(`User with ID ${userId} deleted successfully.`);
      },
      error: (err) => {
        console.error(`Error deleting user with ID ${userId}:`, err);
      }
    });
  }

 

  NavigateToAddNewUser(): void {
    this.router.navigate(['addnewuser']);
  }

  NavigatetoAddNewProject(): void {
    this.router.navigate(['addnewproject']);
  }
}