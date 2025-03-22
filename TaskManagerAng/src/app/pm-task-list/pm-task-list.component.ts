import { Component, OnInit } from '@angular/core';
import { Task } from '../task';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';
import { Project } from '../project';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pm-task-list',
  templateUrl: './pm-task-list.component.html',
  styleUrls: ['./pm-task-list.component.css']
})
export class PmTaskListComponent implements OnInit {
  taskList:Task[]=[];
  ProjList:Project[]=[];
  projid:string[]=[];
  users:UserInfo[]=[];
 

  constructor(private srv:DbAccessService,private router:Router){
  
    }
  ngOnInit():void{
      // var srv = new MydataService();
     this.RefreshView();
     this.GetTasks();
    }
  
    RefreshView():void{
      var LoggedInUser:UserInfo=JSON.parse(sessionStorage.getItem('LoggedinUserData')as string);
      this.srv.GetAllProjects().subscribe({
        next: (res) => {
          var projList1:Project[] = <Project[]>res;
          this.ProjList = projList1.filter((u) => u.pm == LoggedInUser.id);
          this.projid = this.ProjList.map(proj => proj.id).filter((id): id is string => id !== undefined);
          console.log(this.projid) // Filter projects by user ID
        },
        error: (err) => {
          console.log(err);
        }
      });
      this.srv.GetAllUsers().subscribe({
        next: (res)=>{
          var user:UserInfo[]=<UserInfo[]>res;
          this.users=user.filter((u)=>u.role=="QA" || u.role=="DEVELOPER")
        }
      })
      this.GetTasks();
    }
    GetTasks():void{
      this.srv.GetAllTasks().subscribe({next:(res)=>{
        var taskList1:Task[]=<Task[]>res;
        
        this.taskList=taskList1.filter((u)=> this.projid.includes(u.projid))

      },
      error:(err)=>{
        console.log(err);
      }
    });
    }
    NavToAddNewTask():void{
      this.router.navigate(['addtask',{id:this.projid}])
    }
}
