import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { ProjInfo } from '../proj-info';

@Component({
  selector: 'app-pm-dashboard',
  templateUrl: './pm-dashboard.component.html',
  styleUrls: ['./pm-dashboard.component.css']
})
export class PmDashboardComponent implements OnInit {
  projects: ProjInfo[] = [];
  pmId: string | undefined;
  selectedProject: ProjInfo | null = null;

  constructor(private router: Router, private dbAccessService: DbAccessService) {}

  ngOnInit(): void {
    const loggedInUserData = sessionStorage.getItem('LoggedinUserData');
    if (loggedInUserData) {
      const user = JSON.parse(loggedInUserData);
      this.pmId = user.id;
      if (this.pmId) {
        this.getProjectsForPm(this.pmId);
      } else {
        console.error('Project Manager ID is undefined');
      }
    } else {
      console.error('No logged-in user data found');
    }
  }

  NavigateToAddNewProject() {
    this.router.navigate(['addnewproject']);
  }

  getProjectsForPm(pmId: string) {
    this.dbAccessService.GetProjectsForPm(pmId).subscribe((data: ProjInfo[]) => {
      this.projects = data;
    });
  }

  viewMembers(project: ProjInfo) {
    this.selectedProject = project;
  }

  closeMembersView() {
    this.selectedProject = null;
  }
  
  navigateToAssignMembers(): void {
    this.router.navigate(['/assignTeam']);
  }

}
