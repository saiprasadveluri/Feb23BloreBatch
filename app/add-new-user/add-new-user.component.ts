import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserInfo } from '../user-info';
import { DbAccessService } from '../db-access.service';
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
    })
  }
  OnAddClick()
  {
    var name=this.frmGroup.controls['name'].value;
    var email=this.frmGroup.controls['email'].value;
    var password=this.frmGroup.controls['password'].value;
    var role=this.frmGroup.controls['role'].value;
    var newObj:UserInfo={
      name:name,
      email:email,
      password:password,
      role:role
    };
    this.srv.AddNewUser(newObj).subscribe({
      next:(res)=>{
        console.log("New user Added"+res.id);
        this.router.navigate(['admindashboard']);
      },
      error:(err)=>{
 
      }
 
    });
  }
}
