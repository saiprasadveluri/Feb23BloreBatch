import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';

@Component({
  selector: 'app-member-dashboard',
  templateUrl: './member-dashboard.component.html',
  styleUrls: ['./member-dashboard.component.css']
})
export class MemberDashboardComponent implements OnInit {
  assignedProjects: any[] = []; // List of projects assigned to the member

  constructor(private srv: DbAccessService, private router: Router) {}

  ngOnInit(): void {
    this.getAssignedProjects();
  }

  // Fetch projects assigned to the logged-in member
  getAssignedProjects(): void {
    const loggedInUser = JSON.parse(sessionStorage.getItem('LoggedinUserData') || '{}');
    const memberId = loggedInUser.id;

    this.srv.getProjectsForMember(memberId).subscribe({
      next: (res: any[]) => {
        this.assignedProjects = res;
      },
      error: (err: any) => {
        console.error('Error fetching assigned projects:', err);
      }
    });
  }

  // Navigate to the "Add New Project" page
  NavigateToAddNewProject(): void {
    this.router.navigate(['addnewproject']);
  }
}