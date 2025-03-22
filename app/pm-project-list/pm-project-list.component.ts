import { Component , OnInit} from '@angular/core';
import { Project } from '../project';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';
import { ProjectMember } from '../project-member';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pm-project-list',
  standalone: false,
  templateUrl: './pm-project-list.component.html',
  styleUrl: './pm-project-list.component.css'
})
export class PmProjectListComponent implements OnInit{
  projectList:Project[]=[];
  selectedProjectMembers: ProjectMember[] = [];
  selectedProjectId: string | null = null;
  selectedProjectTasks: any[] = [];
  constructor(private srv:DbAccessService, private route:Router){

  }
  ngOnInit(): void {
    this.RefreshView();
  }

  RefreshView():void{
    this.srv.GetAllProjects().subscribe({
      next:(res)=>{
        console.log('check');
        var allprojects=<Project[]>res;
        console.log('All Projects:',allprojects);
        var curUser=<UserInfo>JSON.parse(sessionStorage.getItem('LoggedInUserData')||'{}');
        console.log('Current User:',curUser);
        this.projectList=allprojects.filter((p)=>p.pm==curUser.id);
        console.log('Project List:',this.projectList);
        
      },
      error:(err)=>{
        console.log(err);
      }
    });

    
  }
   

  
  GetMembersForProject(projectId: string): void {
    if (!projectId) {
      console.error('Project ID is undefined');
      return;
    }
    this.srv.GetProjectMembers(projectId).subscribe({
      next: (members) => {
        this.selectedProjectMembers = <ProjectMember[]>members;
        this.selectedProjectId = projectId;
        console.log('Members for Project:', this.selectedProjectMembers);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  GetTasksForProject(projectId: string | undefined): void {
    if (!projectId) {
      console.error('Project ID is undefined');
      return;
    }
    this.selectedProjectId = projectId;
    this.srv.GetProjectTasks(projectId).subscribe({
      next: (tasks) => {
        this.selectedProjectTasks = tasks;
        console.log('Tasks for Project:', this.selectedProjectTasks);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  NavigateToAssignMembers(projectId:string): void { 
    this.route.navigate(['assignmembers', projectId]);
  }

  NavigateToAddTask(projectId: string): void {
    this.route.navigate(['addnewtask', projectId]);
  }


}
