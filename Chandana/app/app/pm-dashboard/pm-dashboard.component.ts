import { Component } from '@angular/core';
import { Router } from '@angular/router';
 
@Component({
  selector: 'app-pm-dashboard',
  templateUrl: './pm-dashboard.component.html',
  styleUrls: ['./pm-dashboard.component.css']
})
export class PmDashboardComponent {
constructor(private router:Router)
  {
   
  }
  goToLoginPage(): void {
    this.router.navigate(['login']);
  }
  NavigateToViewMembers()
  {
    this.router.navigate(['memberdashboard']);
  }
  NavigateToViewProjects()
  {
    this.router.navigate(['viewownprojects']);
  }
 
  NavigateToAssignMember()
  {
    this.router.navigate(['assignmember']);
  }
  NavigateToCreateTask()
  {
    this.router.navigate(['createtask']);
  }
  NavigateToViewTask()
  {
    this.router.navigate(['viewtask']);
  }
  
}