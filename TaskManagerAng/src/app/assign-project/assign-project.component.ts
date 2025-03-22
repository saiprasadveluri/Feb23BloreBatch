import { Component } from '@angular/core';
import { FormBuilder,FormControl,FormGroup,Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { UserInfo } from '../user-info';
import { Projectmember } from '../projectmember';
import { Projectinfo } from '../projectinfo';

@Component({
  selector: 'app-assign-project',
  templateUrl: './assign-project.component.html',
  styleUrls: ['./assign-project.component.css']
})
export class AssignProjectComponent {
frmGroup:FormGroup;
constructor(private fb:FormBuilder,private srv:DbAccessService,private router:Router){
this.frmGroup=fb.group({
  projectid:new FormControl('',[Validators.required]),
  memberid:new FormControl('',[Validators.required])
});
}

projects:Projectinfo[]=[];
prjmem:Projectmember[]=[];
users:UserInfo[]=[];

OnAddClick(){
  var projectid=this.frmGroup.controls['projectid'].value;
  var memberid=this.frmGroup.controls['memberid'].value;
  var newObj1:Projectmember={
    projectid: projectid,
    memberid: memberid
  };
  this.srv.AddNewProjectMember(newObj1).subscribe({
    next:(res)=>{
      console.log("New Project Member Added : " +res.id);
      this.router.navigate(['pmdashboard']);
    },error:(err)=>{
      console.log(err);
    }
  });
}
Refreshview(){
  this.srv.GetAllUsers().subscribe({
    next:(res)=>{
      var user1:UserInfo[]=<UserInfo[]>res;
      this.users=user1.filter((u)=>u.role=='DEV'||u.role=='QA');
    },error:(err)=>{
      console.log(err);
    }
  });
  var LoggedinUserData:UserInfo=JSON.parse(sessionStorage.getItem('LoggedinUserData')as string);
  var id=LoggedinUserData.id;
  this.srv.GetAllProjects().subscribe({
    next:(res)=>{
      var project1=<Projectinfo[]>res;
      this.projects=project1.filter((u)=>u.pm==id);
    },error:(err)=>{
      console.log(err);
    }
  });
}
ngOnInit(){
  this.Refreshview();
}



}
