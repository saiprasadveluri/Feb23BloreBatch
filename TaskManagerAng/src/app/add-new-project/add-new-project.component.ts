import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Projectinfo } from '../projectinfo';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { UserInfo } from '../user-info';
@Component({
  selector: 'app-add-new-project',
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})
export class AddNewProjectComponent {
  projectList:Projectinfo[]=[];
  userList:UserInfo[]=[];
  frmGroup:FormGroup;
  constructor(private fb:FormBuilder,private srv:DbAccessService,private router:Router){
  this.frmGroup=fb.group({
    name:new FormControl('',[Validators.required]),
    pm:new FormControl('',[Validators.required]),
  });
}



OnAddClick(){
  var uname=this.frmGroup.controls['name'].value;
  var upm=this.frmGroup.controls['pm'].value;
  
  var newObj:Projectinfo={
    name: uname,
    pm: upm,
    
  };
  this.srv.AddNewProject(newObj).subscribe({
    next:(res)=>{
      console.log("New Project Added : " +res.id);
      this.router.navigate(['admindashboard']);
    },
    error:(err)=>{

    }
  })

}
ngOnInit():
void{
this.srv.GetAllUsers().subscribe({
  next:(res)=>{
    var user1:UserInfo[]=<UserInfo[]>res;
    this.userList=user1.filter((u)=>u.role=='PM');
  },error:(err)=>
  {
    console.log('Error fetching user list:',err);
   
  }
 
})
}
}
