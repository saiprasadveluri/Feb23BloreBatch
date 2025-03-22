import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { ProjectInfo } from '../project-info';
 
@Component({
  selector: 'app-view-projects',
  templateUrl: './view-projects.component.html',
  styleUrls: ['./view-projects.component.css']
})
export class ViewProjectsComponent implements OnInit {
  projects: ProjectInfo[] = [];
 
  constructor(private dbService: DbAccessService) {}
 
  ngOnInit() {
    this.dbService.GetAllProjects().subscribe((data: ProjectInfo[]) => {
      this.projects = data;
    });
  }
}