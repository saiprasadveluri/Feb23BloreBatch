import { Component, OnInit } from '@angular/core';
import { Project } from '../project';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';
import { FormGroup, FormControl } from '@angular/forms';


@Component({
  selector: 'app-project-list',
  standalone: false,
  templateUrl: './project-list.component.html',
  styleUrl: './project-list.component.css'
})
export class ProjectListComponent implements OnInit{
  projectList:Project[]=[];
  pmList:UserInfo[]=[];
  
    constructor(private srv:DbAccessService){
  
    }
    ngOnInit(): void {
      this.RefreshView();
    }
  
    RefreshView():void{
      this.srv.GetAllProjects().subscribe({
        next:(res)=>{
          this.projectList=<Project[]>res;
          console.log('Project List:',this.projectList);
          
        },
        error:(err)=>{
          console.log(err);
        }
      });

      this.srv.GetAllUsers().subscribe({
        next:(res)=>{
          var allusers = <UserInfo[]>res;
          this.pmList= allusers.filter((u)=>u.role.toUpperCase()=='PM');
       
            console.log('PM List:', this.pmList);

          
          
        }
      })
    }

     
}


  
    

