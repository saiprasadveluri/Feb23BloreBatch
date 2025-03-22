// import { Component, OnInit } from '@angular/core';
// import { Router } from '@angular/router';
// import { DbAccessService } from '../db-access.service';
// import { UserInfo } from '../user-info';
// import { ProjectInfo } from '../project-info';
// import { TaskInfo } from '../task-info';

// @Component({
//   selector: 'app-pm-dashboard',
//   templateUrl: './pm-dashboard.component.html',
//   styleUrls: ['./pm-dashboard.component.css']
// })
// export class PmDashboardComponent implements OnInit {
//   projects: ProjectInfo[] = [];
//   users: UserInfo[] = [];
//   tasks: TaskInfo[] = [];
//   selectedProjectId: string | null = null;

//   constructor(private router: Router, private srv: DbAccessService) {}

//   ngOnInit() {
//     const loggedInUser = JSON.parse(sessionStorage.getItem('LoggedinUserData') || '{}');
//     this.loadProjects(loggedInUser.id);
//     this.loadUsers();
//   }

//   loadProjects(pmId: string) {
//     this.srv.getProjectsByPM(pmId).subscribe({
//       next: (data: ProjectInfo[]) => {
//         this.projects = data;
//       },
//       error: (error: any) => {
//         console.error('Error loading projects', error);
//       }
//     });
//   }

//   loadUsers() {
//     this.srv.getAllUsers().subscribe({
//       next: (data: UserInfo[]) => {
//         this.users = data;
//       },
//       error: (error: any) => {
//         console.error('Error loading users', error);
//       }
//     });
//   }

//   viewProjectMembers(projectId: string) {
//     this.selectedProjectId = projectId;
//     sessionStorage.setItem('selectedProjectId', projectId); // Store selected project ID in session storage
//     this.srv.getTasksByProject(projectId).subscribe({
//       next: (data: TaskInfo[]) => {
//         this.tasks = data;
//       },
//       error: (error: any) => {
//         console.error('Error loading tasks', error);
//       }
//     });
//   }

//   assignMemberToProject(memberId: string) {
//     if (this.selectedProjectId) {
//       const projectId = this.selectedProjectId as string;
//       this.srv.assignMemberToProject(projectId, memberId).subscribe({
//         next: (res: any) => {
//           console.log('Member assigned successfully');
//           this.viewProjectMembers(projectId); // Refresh the project members
//         },
//         error: (error: any) => {
//           console.error('Error assigning member', error);
//         }
//       });
//     }
//   }

//   removeMemberFromProject(memberId: string) {
//     if (this.selectedProjectId) {
//       const projectId = this.selectedProjectId as string;
//       this.srv.removeMemberFromProject(projectId, memberId).subscribe({
//         next: (res: any) => {
//           console.log('Member removed successfully');
//           this.viewProjectMembers(projectId); // Refresh the project members
//         },
//         error: (error: any) => {
//           console.error('Error removing member', error);
//         }
//       });
//     }
//   }

//   createTask(taskName: string, assigneeId: string) {
//     if (this.selectedProjectId) {
//       const projectId = this.selectedProjectId as string;
//       const newTask: TaskInfo = {
//         id: '', // Assign a proper id value here
//         name: taskName,
//         projectId: projectId,
//         assigneeId: assigneeId,
//         status: 'ACTIVE'
//       };
//       this.srv.createTask(newTask).subscribe({
//         next: (res: any) => {
//           console.log('Task created successfully');
//           this.viewProjectMembers(projectId); // Refresh the tasks
//         },
//         error: (error: any) => {
//           console.error('Error creating task', error);
//         }
//       });
//     }
//   }

//   navigateToAddNewProject() {
//     this.router.navigate(['addnewproject']);
//   }

//   navigateToAddTask() {
//     this.router.navigate(['add-task']);
//   }
// }

// import { Component, OnInit } from '@angular/core';
// import { Router } from '@angular/router';
// import { DbAccessService } from '../db-access.service';
 
// @Component({
//   selector: 'app-pm-dashboard',
//   templateUrl: './pm-dashboard.component.html',
//   styleUrls: ['./pm-dashboard.component.css']
// })
// export class PmDashboardComponent implements OnInit {
//   projectList: any[] = []; // List of projects assigned to the PM
//   memberList: any[] = []; // List of all members filtered by role (DEVELOPER and QA)
 
//   constructor(private srv: DbAccessService, private router: Router) {}
 
//   ngOnInit(): void {
//     const loggedInUser = JSON.parse(sessionStorage.getItem('LoggedinUserData') || '{}');
//     const pmId = loggedInUser.id; // Get the logged-in PM's ID
 
//     this.getProjectsForPM(pmId);
//     this.getAllMembers();
//   }
 
//   // Fetch the list of projects assigned to the logged-in PM
//   getProjectsForPM(pmId: string): void {
//     this.srv.getProjectsForPM(pmId).subscribe({
//       next: (res: any[]) => {
//         this.projectList = res.map(project => ({
//           ...project,
//           selectedMemberId: '', // Add a property for selected member ID
//           assignedMembers: [] // Add a property for assigned members
//         }));
//       },
//       error: (err: any) => {
//         console.error('Error fetching projects for PM:', err);
//       }
//     });
//   }
 
//   // Fetch the list of all members filtered by role (DEVELOPER and QA)
//   getAllMembers(): void {
//     this.srv.GetAllUsers().subscribe({
//       next: (res: any[]) => {
//         // Filter members to only include those with the roles "DEVELOPER" or "QA"
//         this.memberList = res.filter(member => member.role === 'DEVELOPER' || member.role === 'QA');
//       },
//       error: (err: any) => {
//         console.error('Error fetching members:', err);
//       }
//     });
//   }
 
//   // Fetch members assigned to a specific project
//   getAssignedMembers(projectId: string): void {
//     const project = this.projectList.find(p => p.id === projectId);
//     if (!project) return;
 
//     this.srv.getMembersForProject(projectId).subscribe({
//       next: (res: any[]) => {
//         project.assignedMembers = res; // Update the assigned members for the project
//       },
//       error: (err: any) => {
//         console.error('Error fetching assigned members:', err);
//       }
//     });
//   }
 
//   // Assign a member to a project
//   assignMemberToProject(projectId: string, memberId: string): void {
//     if (!memberId) {
//       alert('Please select a member to assign');
//       return;
//     }
 
//     const payload = { projId: projectId, memId: memberId };
//     this.srv.addMemberToProject(payload).subscribe({
//       next: () => {
//         alert(`Member ${memberId} assigned successfully to project ${projectId}`);
//         this.getAssignedMembers(projectId); // Refresh the assigned members list
//       },
//       error: (err: any) => {
//         console.error('Error assigning member:', err);
//       }
//     });
//   }
 
//   // Navigate to the "Add New Project" page
//   NavigateToAddNewProject(): void {
//     this.router.navigate(['addnewproject']);
//   }
// }

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';
import { ProjectInfo } from '../project-info';
import { TaskInfo } from '../task-info';

@Component({
  selector: 'app-pm-dashboard',
  templateUrl: './pm-dashboard.component.html',
  styleUrls: ['./pm-dashboard.component.css']
})
export class PmDashboardComponent implements OnInit {
  projects: ProjectInfo[] = [];
  users: UserInfo[] = [];
  tasks: TaskInfo[] = [];
  selectedProjectId: string | null = null;

  constructor(private router: Router, private srv: DbAccessService) {}

  ngOnInit() {
    const loggedInUser = JSON.parse(sessionStorage.getItem('LoggedinUserData') || '{}');
    this.loadProjects(loggedInUser.id);
    this.loadUsers();
  }

  loadProjects(pmId: string) {
    this.srv.getProjectsByPM(pmId).subscribe({
      next: (data: ProjectInfo[]) => {
        this.projects = data;
      },
      error: (error: any) => {
        console.error('Error loading projects', error);
      }
    });
  }

  loadUsers() {
    this.srv.getAllUsers().subscribe({
      next: (data: UserInfo[]) => {
        this.users = data;
      },
      error: (error: any) => {
        console.error('Error loading users', error);
      }
    });
  }

  viewProjectMembers(projectId: string) {
    this.selectedProjectId = projectId;
    sessionStorage.setItem('selectedProjectId', projectId); // Store selected project ID in session storage
    this.srv.getTasksByProject(projectId).subscribe({
      next: (data: TaskInfo[]) => {
        this.tasks = data;
      },
      error: (error: any) => {
        console.error('Error loading tasks', error);
      }
    });
  }

  assignMemberToProject(memberId: string) {
    if (this.selectedProjectId) {
      const projectId = this.selectedProjectId as string;
      this.srv.assignMemberToProject(projectId, memberId).subscribe({
        next: (res: any) => {
          console.log('Member assigned successfully');
          this.viewProjectMembers(projectId); // Refresh the project members
        },
        error: (error: any) => {
          console.error('Error assigning member', error);
        }
      });
    }
  }

  removeMemberFromProject(memberId: string) {
    if (this.selectedProjectId) {
      const projectId = this.selectedProjectId as string;
      this.srv.removeMemberFromProject(projectId, memberId).subscribe({
        next: (res: any) => {
          console.log('Member removed successfully');
          this.viewProjectMembers(projectId); // Refresh the project members
        },
        error: (error: any) => {
          console.error('Error removing member', error);
        }
      });
    }
  }

  createTask(taskName: string, assigneeId: string) {
    if (this.selectedProjectId) {
      const projectId = this.selectedProjectId as string;
      const newTask: TaskInfo = {
        id: '', // Assign a proper id value here
        name: taskName,
        projectId: projectId,
        assigneeId: assigneeId,
        status: 'ACTIVE'
      };
      this.srv.createTask(newTask).subscribe({
        next: (res: any) => {
          console.log('Task created successfully');
          this.viewProjectMembers(projectId); // Refresh the tasks
        },
        error: (error: any) => {
          console.error('Error creating task', error);
        }
      });
    }
  }

  navigateToAddNewProject() {
    this.router.navigate(['addnewproject']);
  }

  navigateToAddTask() {
    this.router.navigate(['add-task']);
  }
}