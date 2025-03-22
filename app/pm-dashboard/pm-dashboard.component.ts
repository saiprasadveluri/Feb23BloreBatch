import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { Project } from '../project';
import { Userinfo } from '../userinfo';
import { ProjectMembers } from '../project-members';
import { Task } from '../task';

@Component({
  selector: 'app-pm-dashboard',
  templateUrl: './pm-dashboard.component.html',
  styleUrls: ['./pm-dashboard.component.css']
})
export class PmDashboardComponent implements OnInit {
  projectinfo: Project[] = [];
  pmId: string | null = null;
  members: Userinfo[] = [];
  selectedProjectId: string | null = null;
  projectMembers: ProjectMembers[] = [];
  tasks: Task[]=[];

  constructor(private srv: DbAccessService, private router: Router) {}

  ngOnInit(): void {
    const loggedInUser = sessionStorage.getItem('LoggedinUserData');
    if (loggedInUser) {
      const user = JSON.parse(loggedInUser);
      this.pmId = user.id;
      this.srv.GetAllProjects().subscribe({
        next: (projects: Project[]) => {
          this.projectinfo = projects.filter(project => project.pm === this.pmId);
        },
        error: (err) => {
          console.error('Error fetching projects:', err);
        }
      });
      this.srv.GetAllUsers().subscribe({
        next: (users: Userinfo[]) => {
          this.members = users.filter(user => user.role === 'DEV' || user.role === 'QA');
        },
        error: (err) => {
          console.error('Error fetching users:', err);
        }
      });
    } else {
      console.error('No logged-in user found in sessionStorage.');
    }
  }

  assignMemberToProject(event: Event): void {
    const target = event.target as HTMLSelectElement;
    const memberId = target.value;
    if (this.selectedProjectId && memberId) {
      const projectMember: ProjectMembers = {
        Project: this.selectedProjectId,
        memid: memberId
      };
      this.srv.AssignMemberToProjectMembers(projectMember).subscribe({
        next: (res) => {
          console.log('Member assigned to project:', res);
        },
        error: (err) => {
          console.error('Error assigning member to project:', err);
        }
      });
    }
  }
  viewProjectMembers(projectId: string): void {
    console.log('Fetching project members for projectId:', projectId); 
    this.srv.GetProjectMembers(projectId).subscribe({
        next: (projectMembers: ProjectMembers[]) => {
            this.projectMembers = projectMembers;
            this.projectMembers = this.projectMembers.filter(pm => pm.Project === projectId);
        },
        error: (err) => {
            console.error('Error fetching project members:', err);
        }
    });
}

viewTasks(projectId: string): void {
  console.log('Fetching tasks for projectId:', projectId); 
  this.srv.GetTasksByProjectId(projectId).subscribe({
      next: (tasks: Task[]) => {
          this.tasks = tasks.filter(task => task.projid === projectId);
      },
      error: (err) => {
          console.error('Error fetching tasks:', err);
      }
  });
}

  setSelectedProjectId(projectId: string): void {
    this.selectedProjectId = projectId;
  }

  navigateToTaskManagement(projectId: string): void {

    this.router.navigate(['taskmanagement', projectId]);
  }
}