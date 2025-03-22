import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { Userinfo } from '../userinfo';
import { Project } from '../project';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {
  users:Userinfo[]=[];
  projects:Project[]=[];
  
  constructor(private router:Router,private srv:DbAccessService){
    
  }
 NavigateToAddNewUser(){
this.router.navigate(['addnewuser']);
 }
 NavigateToAddNewProject(){
  this.router.navigate(['addnewproject']);
   }
   
refreshview(){
  this.srv.GetAllUsers().subscribe({
    next:(res)=>{
      this.users=res as Userinfo[];
    },error:(err)=>{
      
    }
    
  });
  
  this.srv.GetAllProjects().subscribe({
    next:(res)=>{
      this.projects=res as Project[];
    },error:(err)=>{
      
    }
    
  });

}
 ngOnInit():void{

  this.refreshview();
  
}

deleteuser(userid:string):void{
  if(!userid){
    console.error("Not Found");
    return;
  }
  this.srv.deleteUser(userid).subscribe({
    next:()=>{
      console.log("Deleted Success");
      this.refreshview();
    },
    error:(err)=>{
      console.log("Not Deleted")
    }
  })

}
getUserName(uid:string):string{
  var user=this.users.find((u)=>u.id==uid);
  return user ? user.name : 'Name NotFound';
}


}
