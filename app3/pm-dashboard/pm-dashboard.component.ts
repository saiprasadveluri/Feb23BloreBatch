import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { ProjectInfo } from '../project-info';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-pm-dashboard',
  templateUrl: './pm-dashboard.component.html',
  styleUrls: ['./pm-dashboard.component.css']
})
export class PmDashboardComponent implements OnInit {
  projects: ProjectInfo[] = [];
  loggedInPMId: string = '';

  constructor(
    private dbService: DbAccessService,
    private authService: AuthService,
    private router: Router
  ) {}

 ngOnInit() {
  const pmId = this.authService.getLoggedInPMId(); 
  if (pmId) {
    this.loggedInPMId = pmId; 
    this.loadProjects(); 
  } else {
    console.error('No logged-in Project Manager found.');
    this.router.navigate(['/login']); 
  }
}

  loadProjects() {
    this.dbService.getProjectsForManager(this.loggedInPMId).subscribe(
      (data) => {
        this.projects = data;
      },
      (error) => {
        console.error('Error fetching projects:', error);
      }
    );
  }

  manageProject(projectId: string) {
    console.log('Navigating to Project Dashboard for project ID:', projectId); // Debugging
    this.router.navigate([`/project-dashboard/${projectId}`]);
  }

}
  
  

