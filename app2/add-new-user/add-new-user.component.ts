import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserInfo } from '../user-info';
import { DBAccessService } from '../dbaccess.service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-add-new-user',
  standalone:false,
  templateUrl: './add-new-user.component.html',
  styleUrls: ['./add-new-user.component.css']
})
export class AddNewUserComponent {
  frmGroup:FormGroup;
  constructor(private fb:FormBuilder,private srv:DBAccessService,private router:Router)
  {
this.frmGroup=fb.group({
  name:new FormControl('', [Validators.required]),
  email:new FormControl('', [Validators.required]),
  password:new FormControl('', [Validators.required]),
  role:new FormControl('', [Validators.required])
});
}
OnAddclick(){
  var uname=this.frmGroup.controls['name'].value;
  var uemail=this.frmGroup.controls['email'].value;
  var upassword=this.frmGroup.controls['password'].value;
  var urole=this.frmGroup.controls['role'].value;
  var newObj:UserInfo={
    name: uname,
    email: uemail,
    password: upassword,
    role: urole,
    id: ''
  };
  this.srv.AddNewUser(newObj).subscribe({
    next:(res)=>{
      console.log("new userAdded:" +res.id);
      this.router.navigate(['admindashboard']);
    },
    error:(err)=>{

    }
  })
}
}