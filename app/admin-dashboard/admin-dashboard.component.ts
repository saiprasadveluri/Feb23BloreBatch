// import { Component,OnInit } from '@angular/core';
// import { Router } from '@angular/router';
// import { DbAccessService } from '../db-access.service';
// import { UserInfo } from '../user-info';
 
// @Component({
//   selector: 'app-admin-dashboard',
//   templateUrl: './admin-dashboard.component.html',
//   styleUrls: ['./admin-dashboard.component.css']
// })
// export class AdminDashboardComponent implements OnInit {
//   users: UserInfo[] = [];
//   constructor(private router:Router,private dbService: DbAccessService)
//   {
   
//   }
//   ngOnInit() {
//     // Fetch the list of users from the backend
//     this.dbService.GetAllUsers().subscribe((data: UserInfo[]) => {
//       this.users = data;
//     });
//   }
//   NavigateToAddNewUser()
//   {
//     this.router.navigate(['addnewuser']);
//   }

//   DeleteUser(userId: number | undefined) {
//     if (userId === undefined) {
//       alert('Invalid user ID');
//       return;
//     }
//     this.dbService.DeleteUser(userId).subscribe(() => {
//       alert('User deleted successfully');
//       // Refresh the user list after deletion
//       this.users = this.users.filter(user => user.id !== userId);
//     });
//   }
 
// }

import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {
  constructor(private router: Router) {}

  NavigateToAddNewUser() {
    this.router.navigate(['addnewuser']); // Replace 'addnewuser' with the actual route for adding a new user
  }

  NavigateToDeleteUser() {
    this.router.navigate(['deleteuser']); // Ensure this matches the route path
  }
  NavigateToAddProject() {
    this.router.navigate(['add-project']);
  }

  NavigateToViewUsers() {
    this.router.navigate(['view-users']);
  }

  NavigateToViewProjects() {
    this.router.navigate(['view-projects']);
  }
}