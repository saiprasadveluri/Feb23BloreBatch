import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';
import { Router } from '@angular/router';
 
@Component({
  selector: 'app-add-new-user',
  templateUrl: './add-new-user.component.html',
  styleUrls: ['./add-new-user.component.css']
})
export class AddNewUserComponent {
  frmGroup:FormGroup;
  constructor(private fb:FormBuilder,private srv:DbAccessService,private router:Router)
  {
    this.frmGroup=fb.group({
      name:new FormControl('',[Validators.required]),
      email:new FormControl('',[Validators.required]),
      password:new FormControl('',[Validators.required]),
      role:new FormControl('',[Validators.required])
    });
  }
  OnAddClick()
  {
    var uname=this.frmGroup.controls['name'].value;
    var uemail=this.frmGroup.controls['email'].value;
    var upassword=this.frmGroup.controls['password'].value;
    var urole=this.frmGroup.controls['role'].value;
    var newObj:UserInfo={
      name:uname,
      email:uemail,
      password:upassword,
      role:urole
    };
    this.srv.AddNewUser(newObj).subscribe({
      next:(res)=>{
        console.log("New User Added:"+res.id);
        this.router.navigate(['admindashboard']);
      },
      error:(err)=>{
 
      }
    })
  }
 
   
  }
 