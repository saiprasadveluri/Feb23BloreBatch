import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DbAccessService } from '../db-access.service';

import { Router } from '@angular/router';
import { TaskInfo } from '../task-info';

@Component({
  selector: 'app-view-task',
  templateUrl: './view-task.component.html',
  styleUrls: ['./view-task.component.css']
})
export class ViewTaskComponent implements OnInit{
    taskList:TaskInfo[]=[];
    constructor(private srv:DbAccessService,private router:Router)
    {
    
    }
  
    goToPMDashboard(): void {
      this.router.navigate(['pmdashboard']); 
    }
    ngOnInit(): void {      
      this.RefreshView();
    }
  
    RefreshView():void
    {
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




