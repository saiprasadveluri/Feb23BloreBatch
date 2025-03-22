import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';
import { Projectinfo } from '../projectinfo';
 
@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {
  users:UserInfo[]=[];
  projects:Projectinfo[]=[];
 
  constructor(private router:Router,private srv:DbAccessService){
   
  }
 NavigateToAddNewUser(){
this.router.navigate(['addnewuser']);
 }
 NavigateToAddNewProject(){
  this.router.navigate(['addnewproject']);
}
DeleteUser(id:string):void{
  this.srv.DeleteUser(id).subscribe({
    next:(res)=>{
      console.log("User Deleted");
      this.ngOnInit();

    },error:(err)=>{
      console.log("Error in deleting user");
    }
  })
}
DeleteProject(id:string):void{
  this.srv.DeleteProject(id).subscribe({
    next:(res)=>{
      console.log("Project Deleted");
      this.ngOnInit();
    },error:(err)=>{
      console.log("Error in deleting project");
    }
  })

}
getusername(id:string):string{
  var user=this.users.find((u)=>u.id==id);
  return user?.name||'';
}

   
 ngOnInit():void{
  this.srv.GetAllUsers().subscribe({
    next:(res)=>{
      this.users=res as UserInfo[];
    },error:(err)=>{
     
    }
   
  })
  this.srv.GetAllProjects().subscribe({
    next:(res)=>{
      this.projects=res as Projectinfo[];
    },error:(err)=>{
     
    }
   
  })
}
 
 
}