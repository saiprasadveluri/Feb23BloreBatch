import { Component } from '@angular/core';
import { FormControl, Validators,FormBuilder,FormGroup } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user.info';
import { Router } from '@angular/router';


@Component({
  selector: 'app-add-new-use',
  templateUrl: './add-new-use.component.html',
  styleUrls: ['./add-new-use.component.css']
})

export class AddNewUseComponent {
  frmGroup:FormGroup;
  constructor(private fb:FormBuilder,private srv:DbAccessService,private router:Router){
    this.frmGroup=fb.group({
    name:new FormControl('',[Validators.required]),
      email:new FormControl('',[Validators.required]),
      password:new FormControl('',[Validators.required]),
      role:new FormControl('',[Validators.required]),

    });
  }
  OnAddClick(){
    var uname=this.frmGroup.controls['name'].value;
    var uemail=this.frmGroup.controls['email'].value;
    var upassword=this.frmGroup.controls['password'].value;
    var urole=this.frmGroup.controls['role'].value;
    var newObj:UserInfo={
      name:uname,
      email:uemail,
      password:upassword,
      role:urole,
    };
    this.srv.AddNewUser(newObj).subscribe({
      next:(res)=>{
        console.log("new user added: "+res.id);
        this.router.navigate(['admindashboard']);

      },
      error:(err)=>{

      }
    })

  }

}
