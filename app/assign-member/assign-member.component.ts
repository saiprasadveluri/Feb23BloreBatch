import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { ProjInfo } from '../proj-info';
import { ProjMember } from '../proj-member';
import { TaskInfo } from '../task-info';

@Component({
  selector: 'app-assign-member',
  templateUrl: './assign-member.component.html',
  styleUrls: ['./assign-member.component.css']
})
export class AssignMemberComponent {
  userId:string="07d3";
  ownprojList:ProjInfo[]=[]; 
  membList:ProjMember[]=[];
  taskList: TaskInfo[]=[];
  constructor(private srv:DbAccessService,private router:Router)
  {
  
  }

  goToPMDashboard(): void {
    this.router.navigate(['pmdashboard']); // Change to your actual PM dashboard route
  }
  ngOnInit(): void {
    this.RefrehView();
    
  }
  
  RefrehView():void{
  this.srv.GetAllProjects().subscribe({
    next:(res)=>{
      var allProjList:ProjInfo[]=<ProjInfo[]>res;
      this.ownprojList=allProjList.filter(r=>r.pm==this.userId)
      
    }
  });
  this.srv.GetAllMembers().subscribe({
    next:(res)=>{
      this.membList=<ProjMember[]>res;
    },
    error:(err)=>{
      console.log(err);
    }
});
this.srv.GetAllTasks().subscribe({
              next:(res)=>{
                this.taskList=<TaskInfo[]>res;
              },
              error:(err)=>{
                console.log(err);
              }
        });
  }
}
