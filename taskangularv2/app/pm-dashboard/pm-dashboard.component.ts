import { Component } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';

interface Tasks{
  "id"?: string,
  "projid": string,
  "title": string,
  "description": string,
  "tasktype": string,
  "assignedto": string,
  "status": string
}

interface Projectinfo {
  id: string;
  name: string;
  pm: string;
}
interface users{
  id?:string;
  name:string;
  email:string;
  password:string;
  role:string;

}
interface Members{
  "id": string,
  "project": string,
  "memid": string
}
@Component({
  selector: 'app-pm-dashboard',
  templateUrl: './pm-dashboard.component.html',
  styleUrls: ['./pm-dashboard.component.css']
})
export class PmDashboardComponent {
  projects: Projectinfo [] = [];
  tasks:Tasks[]=[];
  User:users[]=[];
  Member:Members[]=[];

  constructor(private srv:DbAccessService, private router:Router) { }

  ngOnInit(): void {
    this.getProjects();
    this.retrieveUsers();
   
  }

//   getProjects(): void {
//     // Fetch projects from the database
//     this.srv.GetAllProjects().subscribe({
//       next: (res: Projectinfo[]) => {
//         this.projects=res;
//       },
//       error: (err) => {
//         console.error('Error fetching projects:', err);
//   }

// });
// }
getProjects(): void {

  this.srv.GetAllProjects().subscribe({
    next: (res: Projectinfo[]) => {
      const sessionData = sessionStorage.getItem('LoggedinUserData');
      let sessionId: string | null = null;

      if (sessionData) {
        const filterObj = JSON.parse(sessionData); 
        sessionId = filterObj?.id; 
      }

      if (sessionId) {
        this.projects = res.filter(project => project.pm === sessionId);
      } else {
        console.warn('No valid session data found for comparison');
        this.projects = [];
      }
    },
    error: (err) => {
      console.error('Error fetching projects:', err);
    }
  });
}
retrieveUsers(): void {
  this.srv.GetAllUsers().subscribe({
    next: (res: users[]) => {
      this.User = res.filter(user => user.role.toUpperCase() === 'QA' || user.role.toUpperCase() === 'DEV');
      console.log('Filtered Users:', this.User);
    },
    error: (err) => {
      console.error('Error fetching users:', err);
    }
  });
}
onManageTask(project: { id: string; name: string }) {
  this.srv.setProjectData(project);
  this.router.navigate(['/managetask', project.id]);
}
ViewTasks(project: { id: string; name: string }): void {
  console.log("viewtask is called");
  if (project && project.id) {
    console.log("entered if")
    this.srv.GetTasks().subscribe({
      next: (res: Tasks[]) => {
      
        this.tasks = res.filter(task => task.projid === project.id);
        console.log('Filtered tasks:', this.tasks);
      },
      error: (err) => {
        console.error('Error fetching tasks:', err);
      }
    });
  } else {
    console.warn('No valid project provided to filter tasks');
    this.tasks = []; 
  }
}
onUserSelect(event: Event, project: { id: string; name: string }): void {
  const selectedUserId = (event.target as HTMLSelectElement).value; 
  if (selectedUserId && project.id) {
    const projectMember = {
      id: this.generateUniqueId(),
      project: project.id,
      memid: selectedUserId
    };

   
    this.srv.AddProjectMember(projectMember).subscribe({
      next: (response) => {
        console.log('Project member added successfully:', response);

       
        this.User = this.User.filter(user => user.id !== selectedUserId);

        
        alert(`Member with ID ${selectedUserId} has been assigned to the project.`);
      },
      error: (err) => {
        console.error('Error adding project member:', err);
      }
    });
  } else {
    console.warn('Invalid user or project selection');
  }
}
 generateUniqueId(): string {
  const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
  let uniqueId = '';
  for (let i = 0; i < 4; i++) {
    uniqueId += characters.charAt(Math.floor(Math.random() * characters.length));
  }
  return uniqueId;
}
retrieveMembers(project: { id: string; name: string }): void {
  if (project && project.id) {
    this.srv.GetAllMembers().subscribe({
      next: (res: Members[]) => {
        this.Member = res.filter(member => member.project === project.id);
        console.log(`Members for project ${project.name} (${project.id}):`, this.Member);
      },
      error: (err) => {
        console.error('Error fetching members:', err);
      }
    });
  } else {
    console.warn('Invalid project provided for retrieving members');
    this.Member = []; 
  }
}

}