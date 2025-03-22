import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators ,} from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Projectmember } from '../projectmember';
import { Project } from '../project';
import { Userinfo } from '../userinfo';

@Component({
  selector: 'app-assign-project',
  templateUrl: './assign-project.component.html',
  styleUrls: ['./assign-project.component.css']
})
export class AssignProjectComponent {

frmGroup:FormGroup;
constructor(private fb:FormBuilder,private srv:DbAccessService, private router:Router){
  this.frmGroup=fb.group({
    projectid:new FormControl('',[Validators.required]),
    memid:new FormControl('',[Validators.required])
  });
}

users:Userinfo[]=[];
projects:Project[]=[];
prjmems:Projectmember[]=[];

OnClickAssign(){
  var projectid=this.frmGroup.controls['projectid'].value;
  var memid=this.frmGroup.controls['memid'].value;
  var newObj:Projectmember={
    projectid:projectid,
    memid:memid   
  };
  this.srv.AddNewProjectMember(newObj).subscribe({
    next:(res)=>{
      console.log("New Member to Project Added: "+res.id);
      this.router.navigate(['pmdashboard']);
    },
    error:(err)=>{

    }
  })
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
