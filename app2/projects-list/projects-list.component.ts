import { Component, OnInit } from '@angular/core';
import { DBAccessService } from '../dbaccess.service';
import { Router } from '@angular/router';
import { AddNewProjectComponent } from '../add-new-project/add-new-project.component';
import { ProjectInfo } from '../project-info';

@Component({
  selector: 'app-projects-list',
  standalone:false,
  templateUrl: './projects-list.component.html',
  styleUrls: ['./projects-list.component.css']
})
export class ProjectsListComponent implements OnInit{
  projects: ProjectInfo[] = [];
  loggedInPMId: string = '';
  authService: any;
  id: string | undefined;
  dbService: any;

  
  constructor(private srv: DBAccessService, private router: Router) {}
  ngOnInit(): void {
    this.loadProjects();
  }

  loadProjects(): void {
    this.srv.GetAllProjects().subscribe({
      next: (res) => {
        this.projects = <ProjectInfo[]>res; // Assign the fetched projects to the array
      },
      error: (err) => {
        console.error('Error fetching projects:', err);
      }
    });

}
NavigateToManageTask(){
  this.router.navigate(['managetask'])
}
}



 
 