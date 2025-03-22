import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';
import { Router } from '@angular/router';

@Component({
  selector: 'app-addnewuser',
  standalone: false,
  templateUrl: './addnewuser.component.html',
  styleUrl: './addnewuser.component.css'
})
export class AddnewuserComponent {
  frmGroup:FormGroup;
  constructor(private fb:FormBuilder, private srv:DbAccessService, private router :Router ){ this.frmGroup=fb.group({ 
    uname:new FormControl('', [Validators.required]), 
    uemail:new FormControl('', [Validators.required]),
    upassword:new FormControl('', [Validators.required]),
    urole:new FormControl('', [Validators.required])
  })
 }
  

  OnAddClick(){
    var uname = this.frmGroup.controls['uname'].value;
    var uemail = this.frmGroup.controls['uemail'].value;
    var upassword = this.frmGroup.controls['upassword'].value;
    var urole = this.frmGroup.controls['urole'].value;
    console.log('Name:', uname);
    console.log('Email:', uemail);
    console.log('Password:', upassword);
    console.log('Role:', urole);

    console.log('Form valid:', this.frmGroup.valid); // Logs true or false
    
  
  
    var newObj:UserInfo={
      name:uname,
      email:uemail,
      password:upassword,
      role:urole

    };
    console.log(newObj);

    this.srv.AddNewUser(newObj).subscribe({
      next:(res)=>{
      console.log("New User Added",res.id);
      this.router.navigate(['admindashboard']);
      },
      error:(err)=>{

      }
    })
  }
}
