import { Component } from '@angular/core';
import { UserInfo } from '../user-info';
import { Router } from '@angular/router';
import { Projectinfo } from '../projectinfo';
import { DbAccessService } from '../db-access.service';
import { Projectmember } from '../projectmember';
import { FormBuilder } from '@angular/forms';
import { Tasks } from '../tasks';

@Component({
  selector: 'app-pm-dashboard',
  templateUrl: './pm-dashboard.component.html',
  styleUrls: ['./pm-dashboard.component.css']
})
export class PmDashboardComponent{
    users:UserInfo[]=[];
    pmprojects:Projectinfo[]=[];
    projectmember:Projectmember[]=[];
    filteredUsers:UserInfo[]=[];
    tasks:Tasks[]=[];
   

constructor(private router:Router,private srv:DbAccessService,private fb:FormBuilder)
  {
  }

NavigateToAddNewTask(){
    this.router.navigate(['addnewtask']);
  }
NavigateToAddNewMember(){
    this.router.navigate(['assignprojectmember']);
}
DeleteMember(id:string):void{
    this.srv.DeleteProjectMember(id).subscribe({
        next:(res)=>{
            console.log("Member Deleted");
            this.Refreshview();
        },error:(err)=>{
            console.log("Error in deleting member");
        }
    });
  }
  
  getprojectname(id:string):string{
    var project=this.pmprojects.find((p)=>p.id==id);
    return project?.name||'';
  }
  getTasksname(id:string):string{
    var task=this.users.find((t)=>t.id==id);
    return task?.name||'';
  }
  getname(id:string):string{
    var user=this.users.find((u)=>u.id==id);
    return user?.name||'';

  }
ngOnInit(): void {
   this.Refreshview(); 
     
}
Refreshview(): void {
  const loggedinUserDataString = sessionStorage.getItem('LoggedinUserData');
  if (loggedinUserDataString) {
      const LoggedinUserData: UserInfo = JSON.parse(loggedinUserDataString);
      const id = LoggedinUserData.id;
      this.srv.GetAllUsers().subscribe({
        next: (res) => {
            var user1:UserInfo[]=<UserInfo[]>res;
            this.users=user1.filter((u)=>u.role=='DEV'||u.role=='QA');
        },error:(err)=>{
            console.log('Error fetching user list:', err);
        }
      });
      this.srv.GetAllProjects().subscribe({
          next: (res) => {
              const projects: Projectinfo[] = <Projectinfo[]>res;
              this.pmprojects = projects.filter((p) => p.pm == id);
              console.log(this.pmprojects);
          },
          error: (err) => {
              console.log('Error fetching project list:', err);
          }
      });

      this.srv.GetAllTasks().subscribe({
        next: (res) => {
          this.tasks = <Tasks[]>res;
          console.log(this.tasks);
        },
        error: (err) => {
          console.log('Error fetching tasks:', err);
        }
      });
      this.srv.GetAllProjectMember().subscribe({
          next: (res) => {
              this.projectmember = res as Projectmember[];
              console.log("I Return");
          },
          error: (err) => {
              console.log('Error fetching project list:', err);
          }
      });
  } else {
      console.error('LoggedinUser is not found in session storage');
  }
}
    
}




