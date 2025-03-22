import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {
  constructor(private router:Router)
  {
    
  }
  goToLoginPage(): void {
    this.router.navigate(['login']); 
  }
  NavigateToAddNewUser()
  {
    this.router.navigate(['addnewuser']);
  }
  NavigateToAddNewProject()
  {
    this.router.navigate(['addnewproject']);
  }
  NavigateToViewUsers()
  {
    this.router.navigate(['viewusers']);
  }
  NavigateToViewProjects()
  {
    this.router.navigate(['viewprojects']);
  }
}
