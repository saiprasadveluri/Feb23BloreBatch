import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';

@Component({
  selector: 'app-view-projects',
  templateUrl: './view-projects.component.html',
  styleUrls: ['./view-projects.component.css']
})
export class ViewProjectsComponent implements OnInit {
  projects:any[]=[];
  constructor(private dbAccessService:DbAccessService){

  }
  ngOnInit(): void {
    this.dbAccessService.GetAllProjects().subscribe((data)=>{
      this.projects=data;
    })
  }
}
