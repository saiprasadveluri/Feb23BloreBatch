// import { Component } from '@angular/core';
 // import { Router } from '@angular/router';
 
 // @Component({
 //   selector: 'app-admin-dashboard',
 //   templateUrl: './admin-dashboard.component.html',
 //   styleUrls: ['./admin-dashboard.component.css']
 // })
 // export class AdminDashboardComponent {
 //   constructor(private router:Router)
 //   {
     
 //   }
 //   NavigateToAddNewUser()
 //   {
 //     this.router.navigate(['addnewuser']);
 //   }
 
 // }
 
//  import { Component } from '@angular/core';
//  import { Router } from '@angular/router';
  
//  @Component({
//    selector: 'app-admin-dashboard',
//    templateUrl: './admin-dashboard.component.html',
//    styleUrls: ['./admin-dashboard.component.css']
//  })
//  export class AdminDashboardComponent {
//    constructor(private router: Router) {}
  
//    NavigateToAddNewUser() {
//      this.router.navigate(['addnewuser']); // Replace 'addnewuser' with the actual route for adding a new user
//    }
  
//    NavigateToDeleteUser() {
//      this.router.navigate(['deleteuser']); // Ensure this matches the route path
//    }
//    NavigateToAddProject() {
//      this.router.navigate(['add-project']);
//    }
  
//    NavigateToViewUsers() {
//      this.router.navigate(['view-users']);
//    }
  
//    NavigateToViewProjects() {
//      this.router.navigate(['view-projects']);
//    }
//  }

import { Component } from '@angular/core';
import { Router } from '@angular/router';
 
 
@Component({
  selector: 'app-admindashboard',
  templateUrl: './admindashboard.component.html',
  styleUrls: ['./admindashboard.component.css']
})
export class AdmindashboardComponent {
constructor(private router:Router)
{
 
}
NavigateToAddNewUser()
{
  this.router.navigate(['addnewuser'])
}
NavigateToAddNewProject()
{
  this.router.navigate(['addnewproject'])
}
}