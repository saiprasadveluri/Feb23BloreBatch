import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { ProjectInfo } from '../project-info';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-pm-dashboard',
  templateUrl: './pm-dashboard.component.html',
  styleUrls: ['./pm-dashboard.component.css']
})
export class PmDashboardComponent {
  projects: ProjectInfo[] = [];
  pmId: string = '';

  constructor(private router: Router, private srv: DbAccessService) {}

  ngOnInit(): void {
    const loggedInUserData = sessionStorage.getItem('LoggedInUserData');
    if (loggedInUserData) {
      const currentUser: UserInfo = JSON.parse(loggedInUserData);
      if (currentUser.role.toUpperCase() === 'PM') {
        this.pmId = currentUser.id!;
        this.loadProjects();
      }
    }
  }

  loadProjects(): void {
    this.srv.GetProjects().subscribe({
      next: (projects: ProjectInfo[]) => {
        this.projects = projects.filter(project => project.pm === this.pmId);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
  navigateToCreateTask() {
    this.router.navigate(['create-task']);
  }

  navigateToListTasks() {
    this.router.navigate(['list-tasks']);
  }

  navigateToAddMembers() {
    this.router.navigate(['add-members']);
  }

  logout(): void {
    sessionStorage.clear();
    this.router.navigate(['/login']);
  }
}
