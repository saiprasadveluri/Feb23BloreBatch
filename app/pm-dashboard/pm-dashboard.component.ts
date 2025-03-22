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
  NavigateToViewMembers()
  {
    this.router.navigate(['memberdashboard']);
  }
  NavigateToViewProjects()
  {
    this.router.navigate(['viewownprojects']);
  }

}
