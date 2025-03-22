import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { Task } from '../task';
import { UserInfo } from '../user-info';
import { Router } from '@angular/router';


@Component({
  selector: 'app-member-task-list',
  templateUrl: './member-task-list.component.html',
  styleUrls: ['./member-task-list.component.css']
})
export class MemberTaskListComponent implements OnInit {
  taskList:Task[]=[];
  LoggedInUser:UserInfo=JSON.parse(sessionStorage.getItem('LoggedinUserData')as string);
  status:string='';
  constructor(private srv:DbAccessService,private router:Router){}
  ngOnInit(): void {
    this.srv.GetAllTasks().subscribe({
      next:(res)=>{
        var taskList1:Task[]=<Task[]>res;
        this.taskList=taskList1.filter((u)=>u.assignedto==this.LoggedInUser.id)
        console.log(this.taskList);
        
      }
    })
    
  }
  AddComment(id:string,tit:string):void{
    this.router.navigate(['addcomment',{id:id,cid:this.LoggedInUser.id,title:tit}])
  }
  UpdateTask(id:string,pid:string):void{
    this.router.navigate(['updatetask',{id:id,pid:pid}])
  }
  ChangeStatus(id:string,pid:string):void{
    if(this.LoggedInUser.role == 'QA'){
      this.status='Reopen';
    }
    else{
      this.status='Resolved'
    }
    this.srv.UpdateTaskStatus(id,this.status).subscribe({
      next:(res)=>{
        this.router.navigate(['updatetask',{id:id,pid:pid}])
      },
      error:(err)=>{

      }
    })
    
  }
}
