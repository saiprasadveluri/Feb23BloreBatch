import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Userinfo } from '../userinfo';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-new-user',
  templateUrl: './add-new-user.component.html',
  styleUrls: ['./add-new-user.component.css']
})
export class AddNewUserComponent {
  FrmGroup:FormGroup;
  constructor(private fb:FormBuilder,private srv:DbAccessService,private route:Router)
  {this.FrmGroup=fb.group({
    name:new FormControl('',[Validators.required]),
    email:new FormControl('',[Validators.required]),
    password:new FormControl('',[Validators.required]),
    role:new FormControl('',[Validators.required]),
  });

  }
  OnAddClick()
  {
    var uname=this.FrmGroup.controls['name'].value;
    var uemail=this.FrmGroup.controls['email'].value;
    var upassword=this.FrmGroup.controls['password'].value;
    var urole=this.FrmGroup.controls['role'].value;

    
    var newObj: Userinfo = {
      name: uname,
      email: uemail,
      password: upassword,
      role: urole,
    };

    this.srv.AddNewUser(newObj).subscribe({
      next: (res) => {
        console.log('User added successfully:', res);
        this.route.navigate(['admindashboard']); 
      },
      error: (err) => {
        console.error('Error adding user:', err);
      },
    });
  }

}
