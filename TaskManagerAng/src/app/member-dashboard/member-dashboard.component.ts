import { Component } from '@angular/core';
import { Tasks } from '../tasks';
import { FormBuilder } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-member-dashboard',
  templateUrl: './member-dashboard.component.html',
  styleUrls: ['./member-dashboard.component.css']
})
export class MemberDashboardComponent {
tasks:Tasks[] = [];
users:UserInfo[] = [];
loggedInUserId:string = '';
loggedInUserrole:string = '';
constructor(private fb:FormBuilder,private srv:DbAccessService,private router:Router){
}
getname(id:string):string{
  const task=this.users.find((task)=>task.id===id);
  return task?.name||'';
}

ngOnInit(): void{
  const LoggedinUserData: UserInfo=JSON.parse(sessionStorage.getItem('LoggedinUserData')as string);
  this.loggedInUserId=LoggedinUserData.id||'';
  this.loggedInUserrole=LoggedinUserData.role;
  this.srv.GetAllTasks().subscribe({
    next:(res)=>{
      const allTasks=res as Tasks[];
      this.tasks=allTasks.filter((task)=>task.assignedto===this.loggedInUserId);
      console.log('Filtered tasks for members: ',this.tasks);
    },error:(err)=>{
      console.log('Error while fetching tasks: ',err);
    }
  });
}

navigateToDeveloperPlace():void{
  this.router.navigate(['developerplace']);
}
navigateToQaPlace():void{
  this.router.navigate(['qaplace']);
}

AddComment(taskId:string):void{
  this.router.navigate(['addcomment'],{
    queryParams:{taskId:taskId,commentedby:this.loggedInUserId}
  });
}
}
