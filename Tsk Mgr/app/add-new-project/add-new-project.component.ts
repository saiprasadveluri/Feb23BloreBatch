import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Project } from '../project';
import { Userinfo } from '../userinfo';

@Component({
  selector: 'app-add-new-project',
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})
export class AddNewProjectComponent {
frmGroup:FormGroup;
constructor(private fb:FormBuilder,private srv:DbAccessService, private router:Router){
  this.frmGroup=fb.group({
    name:new FormControl('',[Validators.required]),
    pm:new FormControl('',[Validators.required])
  });
}
OnAddClickP(){
  var uname=this.frmGroup.controls['name'].value;
  var upm=this.frmGroup.controls['pm'].value;
  var newObj:Project={
    name:uname,
    pm:upm   
  };
  this.srv.AddNewProject(newObj).subscribe({
    next:(res)=>{
      console.log("New Project Added: "+res.id);
      this.router.navigate(['admindashboard']);
    },
    error:(err)=>{

    }
  })
}
users:Userinfo[]=[];
ngOnInit():void{
  this.srv.GetAllUsers().subscribe({
    next:(res)=>{
      var user1:Userinfo[]=<Userinfo[]>res;
      this.users=user1.filter((u)=>u.role=='PM');
    },error:(err)=>{
      
    }
    
  })
}
}
