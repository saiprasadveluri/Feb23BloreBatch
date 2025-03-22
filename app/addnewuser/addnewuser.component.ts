import { Component } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { UserInfo } from '../user-info';
import { DBAccessService } from '../dbaccess.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-add-new-user',
  templateUrl: './addnewuser.component.html',
  styleUrls: ['./addnewuser.component.css']
})
export class AddNewUserComponent {
frmGroup:FormGroup;
constructor(private fb:FormBuilder,private srv:DBAccessService,private router:Router) {
  this.frmGroup = this.fb.group({
    name: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
    role: new FormControl('', [Validators.required])
});
}
OnAddClick()
{
  var uname = this.frmGroup.controls['name'].value;
  var uemail = this.frmGroup.controls['email'].value;
  var upassword = this.frmGroup.controls['password'].value;
  var urole = this.frmGroup.controls['role'].value;
  var unewObj:UserInfo={
    name:uname,
    email:uemail,
    password:upassword,
    role:urole
  };
  this.srv.AddNewUser(unewObj).subscribe({
    next:(res)=>{
      console.log("User added successfully"+res.id);
      this.router.navigate(['admindashboard']);
    },
    
}
  );
}
}
 
 