import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { ProjInfo } from '../proj-info';

@Component({
  selector: 'app-view-projects',
  templateUrl: './view-projects.component.html',
  styleUrls: ['./view-projects.component.css']
})
export class ViewProjectsComponent {


  projectList:ProjInfo[]=[];
    constructor(private srv:DbAccessService,private router:Router)
    {
  
    }
    goToPMDashboard(): void {
      this.router.navigate(['admindashboard']); // Change to your actual PM dashboard route
    }
    ngOnInit(): void {      
      this.RefreshView();
    }
  
    
  
    RefreshView():void
    {
      this.srv.GetAllProjects()
        .subscribe({
              next:(res)=>{
                this.projectList=<ProjInfo[]>res;
              },
              error:(err)=>{
                console.log(err);
              }
        });
    }  

}
