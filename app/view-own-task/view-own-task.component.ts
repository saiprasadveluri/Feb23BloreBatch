import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { ProjInfo } from '../proj-info';
import { TaskInfo } from '../task-info';

@Component({
  selector: 'app-view-own-task',
  templateUrl: './view-own-task.component.html',
  styleUrls: ['./view-own-task.component.css']
})
export class ViewOwnTaskComponent {
  userId:string="3";//To Do:
  owntaskList:TaskInfo[]=[]; 
  constructor(private srv:DbAccessService,private router:Router)
  {
  
  }

  goToQaDashboard(): void {
    this.router.navigate(['qadashboard']); 
  }
  ngOnInit(): void {
    this.RefrehView();
    
  }
  
  RefrehView():void{
  this.srv.GetAllTasks().subscribe({
    next:(res)=>{
      var allTaskList:TaskInfo[]=<TaskInfo[]>res;
      this.owntaskList=allTaskList.filter(r=>r.assignedto==this.userId)
      
    }
    
    
  });

  
  }

}











