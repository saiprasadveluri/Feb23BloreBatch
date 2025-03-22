import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-qa-dashboard',
  templateUrl: './qa-dashboard.component.html',
  styleUrls: ['./qa-dashboard.component.css']
})
export class QaDashboardComponent {
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
