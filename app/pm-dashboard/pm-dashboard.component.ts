import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Project } from '../project';
import { Userinfo } from '../userinfo';
import { Projectmember } from '../projectmember';
import { Task } from '../task';

@Component({
  selector: 'app-pm-dashboard',
  templateUrl: './pm-dashboard.component.html',
  styleUrls: ['./pm-dashboard.component.css']
})
export class PmDashboardComponent implements OnInit{
  
constructor(private srv:DbAccessService, private router:Router){
}
users:Userinfo[]=[];
projects:Project[]=[];
prjmems:Projectmember[]=[];
tsks:Task[]=[];

getProjectName(pid:string):string{
          var project=this.projects.find((p)=>p.id==pid);
          return project ? project.name : 'Name NotFound';
      
}
getUserName(uid:string):string{
  var user=this.users.find((u)=>u.id==uid);
  return user ? user.name : 'Name NotFound';

}



  ngOnInit(){
    this.Refreshview();
  }
  Refreshview(){
    var LoggedinUserData:Userinfo=JSON.parse(sessionStorage.getItem('LoggedinUserData' )as string);
    var id=LoggedinUserData.id;
    this.srv.GetAllProjects().subscribe({
        next:(res)=>{
          var project1:Project[]=<Project[]>res;
          this.projects=project1.filter((u)=>u.pm==id);
        },error:(err)=>{       
        }       
      });
      this.srv.GetAllUsers().subscribe({
        next:(res)=>{
          var user1:Userinfo[]=<Userinfo[]>res;
          this.users=user1.filter((u)=>u.role=='DEV'||u.role=='QA');
        },error:(err)=>{ 
        }
      });
    this.srv.GetAllProjectMember().subscribe({
      next:(res)=>{
        var allprjmems= res as Projectmember[];
        this.prjmems = allprjmems.filter(
              (pm) => this.getProjectName(pm.projectid) !== 'Name NotFound'
            );
        console.log("I Return");
      },
      error:(err)=>{
        console.log("I can't Return");
      }
    });

    this.srv.GetAllProjects().subscribe({
      next: (res) => {
        const projects1 = res as Project[];
        this.projects = projects1.filter((u) => u.pm === id);
        console.log('Projects fetched successfully:', this.projects);
        
    this.srv.GetAllTasks().subscribe({
      next: (res) => {
        var allTasks = res as Task[];
        this.tsks = allTasks.filter(
              (t) => this.getProjectName(t.projectid) != 'Unknown Project'
            );
            console.log('Filtered tasks:', this.tsks);
          },
        });
      },
      error: (err) => {
        console.error('Error fetching projects:', err);
      },
    });

  }
  NavigateToAddNewMembers(){
    this.router.navigate(['assignprojectmember']);
     }
}
