import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Userinfo } from '../userinfo';
import { Project } from '../project';
import { Task } from '../task';

@Component({
  selector: 'app-assign-task',
  templateUrl: './assign-task.component.html',
  styleUrls: ['./assign-task.component.css']
})
export class AssignTaskComponent {
frmGroup:FormGroup;
constructor(private fb:FormBuilder,private srv:DbAccessService, private router:Router){
  this.frmGroup=fb.group({
    projectid:new FormControl('',[Validators.required]),
    title:new FormControl('',[Validators.required]),
    description:new FormControl('',[Validators.required]),
    tasktype:new FormControl('',[Validators.required]),
    assignedto:new FormControl('',[Validators.required]),
    status:new FormControl('',[Validators.required])
  });
}

users:Userinfo[]=[];
projects:Project[]=[];
tasks:Task[]=[];

OnClickAssignTask(){
  var projectid=this.frmGroup.controls['projectid'].value;
  var title=this.frmGroup.controls['title'].value;
  var description=this.frmGroup.controls['description'].value;
  var tasktype=this.frmGroup.controls['tasktype'].value;
  var assignedto=this.frmGroup.controls['assignedto'].value;
  var status=this.frmGroup.controls['status'].value;

  var newObj:Task={
    projectid:projectid,
    title:title,
    description:description,
    tasktype:tasktype,
    assignedto:assignedto,
    status:status
  };
  this.srv.AddNewTask(newObj).subscribe({
    next:(res)=>{
      console.log("New Task Added: "+res.id);
      this.router.navigate(['pmdashboard']);
    },
    error:(err)=>{

    }
  });
}
refreshview(){
  this.srv.GetAllUsers().subscribe({
    next:(res)=>{
      var user1:Userinfo[]=<Userinfo[]>res;
      this.users=user1.filter((u)=>u.role=='DEV'||u.role=='QA');
    },error:(err)=>{
      
    }
    
  });
  var LoggedinUserData:Userinfo=JSON.parse(sessionStorage.getItem('LoggedinUserData' )as string);
    var id=LoggedinUserData.id;
    this.srv.GetAllProjects().subscribe({
        next:(res)=>{
          var project1:Project[]=<Project[]>res;
          this.projects=project1.filter((u)=>u.pm==id);
        },error:(err)=>{       
        }       
      });
}
 ngOnInit():void{

  this.refreshview();
  
}

}
