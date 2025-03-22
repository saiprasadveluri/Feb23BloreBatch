import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { Newproject } from '../newproject';

@Component({
  selector: 'app-pm-dashboard',
  templateUrl: './pm-dashboard.component.html',
  styleUrls: ['./pm-dashboard.component.css']
})
export class PmDashboardComponent {
  projectList: any[] = []; // Ensure projectList is initialized as an array
  pmId: string = ''; 
  selectedProjectId: string ='';
  
  memberList: any[]=[];

  constructor(private router:Router,private srv:DbAccessService)
  {
  
  }
  ngOnInit(): void {
    const loggedInUser = JSON.parse(sessionStorage.getItem('LoggedinUserData') || '{}');
    this.pmId = loggedInUser.id; // Get PM ID from session storage
    this.loadProjects();
  }
  loadProjects(): void {
    this.srv.getProjectsForPM(this.pmId).subscribe({
      next: (res: any[]) => {
        this.projectList = res;
        console.log('Projects assigned to PM:', this.projectList);
      },
      error: (err: any) => {
        console.error('Error fetching projects:', err);
      }
    });
  }
  handlePmId(pmId: any) {
    throw new Error('Method not implemented.');
  }
  // Removed duplicate projectList method to resolve the error
  NavigateToAddNewTask()
{
  this.router.navigate(['createtask'])
}
NavigateToAddMembers(projectId: string): void {
  this.router.navigate(['add-project-members', projectId]); // Navigate to Add Project Members page
}

// viewMembers(projectId: string): void {
//   this.router.navigate(['view-members', projectId]);
// }
viewMembers(projectId: string): void {
  this.selectedProjectId = projectId;

  this.srv.getProjectMembers(projectId).subscribe({
    next: (res) => {
      this.memberList = res;
      console.log('Members assigned to project:', this.memberList);
    },
    error: (err) => {
      console.error('Error fetching project members:', err);
    }
  });
}

}
