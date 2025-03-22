// import { Component, OnInit } from '@angular/core';
//  import { DbAccessService } from '../db-access.service';
//  import { ProjectInfo } from '../project-info';
  
//  @Component({
//    selector: 'app-view-projects',
//    templateUrl: './view-projects.component.html',
//    styleUrls: ['./view-projects.component.css']
//  })
//  export class ViewProjectsComponent implements OnInit {
//    projects: ProjectInfo[] = [];
  
//    constructor(private dbService: DbAccessService) {}
  
//    ngOnInit() {
//      this.dbService.GetAllProjects().subscribe((data: ProjectInfo[]) => {
//        this.projects = data;
//      });
//    }
//  }


import { Component, OnInit } from '@angular/core';
import { ViewProjectsComponent as OriginalViewProjectsComponent } from '../view-projects/view-projects.component';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
 
@Component({
  selector: 'app-display-project-list',
  templateUrl: './display-project-list.component.html',
  styleUrls: ['./display-project-list.component.css']
})
export class ViewProjectsComponent implements OnInit {
  projectList: OriginalViewProjectsComponent[] = []; // Define projectList as an array
 
  constructor(private srv: DbAccessService, private router: Router) {}
 
  ngOnInit(): void {
    this.RefreshView();
  }
 
  RefreshView(): void {
    this.srv.DisplayProject().subscribe({
      next: (res: OriginalViewProjectsComponent[]) => {
        this.projectList = res; // Assign the response to projectList
        console.log('Updated project list:', this.projectList);
      },
      error: (err: any) => {
        console.log(err);
      }
    });
  }
 
  AssignTo(projectId: string | undefined): void {
    this.router.navigate(['assignto', projectId]);
  }
}