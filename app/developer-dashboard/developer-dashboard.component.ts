import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-developer-dashboard',
  templateUrl: './developer-dashboard.component.html',
  styleUrls: ['./developer-dashboard.component.css']
})
export class DeveloperDashboardComponent {
  constructor(private router:Router)
  {
    
  }
  goToLoginPage(): void {
    this.router.navigate(['login']); 
  }
  NavigateToViewComment()
  {
    this.router.navigate(['viewcomment']);
  }
  NavigateToAddComment()
  {
    this.router.navigate(['addcomment']);
  }

  NavigateToReassignTask()
  {
    this.router.navigate(['reassigntask']);
  }
  NavigateToViewOwnTask()
  {
    this.router.navigate(['viewowntask']);
  }

}



