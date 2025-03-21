// // import { Component, OnInit } from '@angular/core';
// // import { Router } from '@angular/router';


// // @Component({
// //   selector: 'app-pm-dashboard',
// //   templateUrl: './pm-dashboard.component.html',
// //   styleUrls: ['./pm-dashboard.component.css']
// // })
// // export class PmDashboardComponent {
 
// //   constructor(private router:Router)
// //     {
     
// //     }
    
// //     NavigateToAddNewProject()
// //     {
// //       this.router.navigate(['addnewproject']);
// //     }
// //     NavigateToViewOwnProjects() {
// //       this.router.navigate(['view-my-projects']);
// //     }
  
// //     NavigateToViewMembers() {
// //       this.router.navigate(['view-project-members']);
// //     }
  
// //     NavigateToAssignMembers() {
// //       this.router.navigate(['assign-members']);
// //     }
  
// //     NavigateToRemoveMembers() {
// //       this.router.navigate(['remove-members']);
// //     }
  
// //     NavigateToCreateTask() {
// //       this.router.navigate(['create-task']);
// //     }
  
// //     NavigateToViewTasks() {
// //       this.router.navigate(['view-tasks']);
// //     }
    

   

// // }

// // import { Component, OnInit } from '@angular/core';
// // import { Router } from '@angular/router';
// // import { DbAccessService } from '../db-access.service';
// // import { ProjectInfo } from '../project-info';

// // @Component({
// //   selector: 'app-pm-dashboard',
// //   templateUrl: './pm-dashboard.component.html',
// //   styleUrls: ['./pm-dashboard.component.css']
// // })
// // export class PmDashboardComponent implements OnInit {
// //   projects: ProjectInfo[] = [];
// //   loggedInPmId = '2'; // Replace this with the actual logged-in PM's ID (e.g., from authentication service)

// //   constructor(private router: Router, private dbService: DbAccessService) {}

// //   ngOnInit(): void {
// //     // Fetch all projects and filter them by the logged-in PM's ID
// //     this.dbService.GetAllProjects().subscribe(
// //       (data: ProjectInfo[]) => {
// //         this.projects = data.filter(project => project.pm === Number(this.loggedInPmId));
// //       },
// //       (error) => {
// //         console.error('Error fetching projects:', error);
// //       }
// //     );
// //   }

// //   NavigateToAddNewProject() {
// //     this.router.navigate(['addnewproject']);
// //   }

// //   NavigateToViewOwnProjects() {
// //     this.router.navigate(['view-my-projects']);
// //   }

// //   NavigateToViewMembers() {
// //     this.router.navigate(['view-project-members']);
// //   }

// //   NavigateToAssignMembers() {
// //     this.router.navigate(['assign-members']);
// //   }

// //   NavigateToRemoveMembers() {
// //     this.router.navigate(['remove-members']);
// //   }

// //   NavigateToCreateTask() {
// //     this.router.navigate(['create-task']);
// //   }

// //   NavigateToViewTasks() {
// //     this.router.navigate(['view-tasks']);
// //   }
// // }

// // import { Component, OnInit } from '@angular/core';
// // import { DbAccessService } from '../db-access.service';
// // import { ProjectInfo } from '../project-info';

// // @Component({
// //   selector: 'app-pm-dashboard',
// //   templateUrl: './pm-dashboard.component.html',
// //   styleUrls: ['./pm-dashboard.component.css']
// // })
// // export class PmDashboardComponent implements OnInit {
// //   projects: ProjectInfo[] = [];
// //   loggedInPmId = '3'; // Replace this with the actual logged-in PM's ID (e.g., from authentication service)

// //   constructor(private dbService: DbAccessService) {}

// //   ngOnInit(): void {
// //     // Fetch all projects and filter them by the logged-in PM's ID
// //     this.dbService.GetAllProjects().subscribe({
// //       next: (data: ProjectInfo[]) => {
// //         this.projects = data.filter(project => project.pm === Number(this.loggedInPmId));
// //       },
// //       error: (error) => {
// //         console.error('Error fetching projects:', error);
// //       },
// //       complete: () => {
// //         console.log('Project fetching completed.');
// //       }
// //     });
// //   }
// // }

// // import { Component, OnInit } from '@angular/core';
// // import { Router } from '@angular/router';
// // import { DbAccessService } from '../db-access.service';
// // import { ProjectInfo } from '../project-info';

// // @Component({
// //   selector: 'app-pm-dashboard',
// //   templateUrl: './pm-dashboard.component.html',
// //   styleUrls: ['./pm-dashboard.component.css']
// // })
// // export class PmDashboardComponent implements OnInit {
// //   projects: ProjectInfo[] = [];
// //   loggedInPmId = '3'; // Replace this with the actual logged-in PM's ID (e.g., from authentication service)

// //   constructor(private router: Router, private dbService: DbAccessService) {}

// //   ngOnInit(): void {
// //     // Fetch all projects and filter them by the logged-in PM's ID
// //     this.dbService.GetAllProjects().subscribe({
// //       next: (data: ProjectInfo[]) => {
// //         // Filter projects where the PM ID matches the logged-in PM's ID
// //         this.projects = data.filter(project => project.pm === Number(this.loggedInPmId));
// //       },
// //       error: (error) => {
// //         console.error('Error fetching projects:', error);
// //       }
// //     });
// //   }

// //   NavigateToAddNewProject() {
// //     this.router.navigate(['addnewproject']);
// //   }

// //   NavigateToViewOwnProjects() {
// //     this.router.navigate(['view-my-projects']);
// //   }

// //   NavigateToViewMembers() {
// //     this.router.navigate(['view-project-members']);
// //   }

// //   NavigateToAssignMembers() {
// //     this.router.navigate(['assign-members']);
// //   }

// //   NavigateToRemoveMembers() {
// //     this.router.navigate(['remove-members']);
// //   }

// //   NavigateToCreateTask() {
// //     this.router.navigate(['create-task']);
// //   }

// //   NavigateToViewTasks() {
// //     this.router.navigate(['view-tasks']);
// //   }
// // }

// import { Component, OnInit } from '@angular/core';
// import { DbAccessService } from '../db-access.service';
// import { ProjectInfo } from '../project-info';
// import { AuthService } from '../auth.service';
// import { Router } from '@angular/router'; // Example authentication service

// @Component({
//   selector: 'app-pm-dashboard',
//   templateUrl: './pm-dashboard.component.html',
//   styleUrls: ['./pm-dashboard.component.css']
// })
// export class PmDashboardComponent implements OnInit {
//   projects: ProjectInfo[] = [];
//   members: any[] = [];
//   loggedInPmId: string = ''; // Dynamically set the logged-in PM's ID
//   showProjects = false;

//   constructor(private dbService: DbAccessService, private authService: AuthService,private router:Router) {}

//   ngOnInit(): void {
//     // Get the logged-in PM's ID from the authentication service
//     this.loggedInPmId = this.authService.getLoggedInUserId(); // Replace with your actual logic

//     // Fetch projects assigned to the logged-in PM
//     this.dbService.GetProjectsByPM(this.loggedInPmId).subscribe({
//       next: (data: ProjectInfo[]) => {
//         this.projects = data;
//       },
//       error: (error) => {
//         console.error('Error fetching projects:', error);
//       }
//     });
//   }

//   ShowMyProjects(): void {
//     this.showProjects = true;
//   }

//   GetMembersByProject(projid: number | string | undefined): void {
//     if (projid === undefined) {
//       console.error('Invalid project ID');
//       return;
//     }
//     const projidStr = String(projid); // Convert to string
//     this.dbService.GetMembersByProject(projidStr).subscribe({
//       next: (data: any[]) => {
//         this.members = data;
//       },
//       error: (error) => {
//         console.error('Error fetching project members:', error);
//       }
//     });
//   }

//   NavigateToAddNewProject() {
//     this.router.navigate(['addnewproject']);
//   }

//   NavigateToViewOwnProjects() {
//     this.router.navigate(['view-my-projects']);
//   }

//   NavigateToViewMembers() {
//     this.router.navigate(['view-project-members']);
//   }

//   NavigateToAssignMembers() {
//     this.router.navigate(['assign-members']);
//   }

//   NavigateToRemoveMembers() {
//     this.router.navigate(['remove-members']);
//   }

//   NavigateToCreateTask() {
//     this.router.navigate(['create-task']);
//   }

//   NavigateToViewTasks() {
//     this.router.navigate(['view-tasks']);
//   }
// }
import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { ProjectInfo } from '../project-info';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pm-dashboard',
  templateUrl: './pm-dashboard.component.html',
  styleUrls: ['./pm-dashboard.component.css']
})
export class PmDashboardComponent implements OnInit {
  projects: ProjectInfo[] = []; // Holds the list of projects assigned to the logged-in PM
  members: any[] = []; // Holds the list of members for a selected project
  loggedInPmId: string = ''; // Dynamically set the logged-in PM's ID
  showProjects = false; // Controls whether the projects table is displayed

  constructor(
    private dbService: DbAccessService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Get the logged-in PM's ID from the authentication service
    this.loggedInPmId = this.authService.getLoggedInUserId(); // Replace with your actual logic

    // Fetch projects assigned to the logged-in PM
    this.dbService.GetProjectsByPM(this.loggedInPmId).subscribe({
      next: (data: ProjectInfo[]) => {
        this.projects = data; // Assign the filtered projects to the `projects` array
      },
      error: (error) => {
        console.error('Error fetching projects:', error);
      }
    });
  }

  ShowMyProjects(): void {
    this.showProjects = true; // Set to true to display the projects table
  }

  GetMembersByProject(projid: number | string | undefined): void {
    if (projid === undefined) {
      console.error('Invalid project ID');
      return;
    }
    const projidStr = String(projid); // Convert to string
    this.dbService.GetMembersByProject(projidStr).subscribe({
      next: (data: any[]) => {
        this.members = data; // Assign the fetched members to the `members` array
      },
      error: (error) => {
        console.error('Error fetching project members:', error);
      }
    });
  }

  NavigateToAddNewProject(): void {
    this.router.navigate(['addnewproject']);
  }

  NavigateToViewOwnProjects(): void {
    this.router.navigate(['view-my-projects']);
  }

  NavigateToViewMembers(): void {
    this.router.navigate(['view-project-members']);
  }

  NavigateToAssignMembers(): void {
    this.router.navigate(['assign-members']);
  }

  NavigateToRemoveMembers(): void {
    this.router.navigate(['remove-members']);
  }

  NavigateToCreateTask(): void {
    this.router.navigate(['create-task']);
  }

  NavigateToViewTasks(): void {
    this.router.navigate(['view-tasks']);
  }
}