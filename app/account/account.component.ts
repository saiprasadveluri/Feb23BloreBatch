import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Userinfo } from '../userinfo';
import { Router } from '@angular/router';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent {
frmGroup:FormGroup;
constructor(private fb:FormBuilder,private srv:DbAccessService,private router:Router){
  this.frmGroup=fb.group({
    uemail:new FormControl('',[Validators.required]),
    upassword:new FormControl('',[Validators.required]),
  });
}
OnLoginClick(){
  var uemail=this.frmGroup.controls['uemail'].value;
  var upassword=this.frmGroup.controls['upassword'].value;
  console.log(uemail);
  this.srv.GetAllUsers().subscribe({
    next:(res)=>{
      var AllUsers=<Userinfo[]>res;
      console.log(AllUsers);
      var filterobj=AllUsers.find((u)=>(u.email==uemail && u.password==upassword));
      if(filterobj!=undefined){
        console.log("Success");
        //store the users data into storage
        sessionStorage.setItem('LoggedinUserData',JSON.stringify(filterobj));
        switch(filterobj.role.toUpperCase()){
          case 'ADMIN':{
            this.router.navigate(['admindashboard']);
            break;
          }
          case 'PM':{
            this.router.navigate(['pmdashboard']);
            break;
          }
          case 'MEMBER':{
            this.router.navigate(['memberdashboard']);
            break;
          }
        }       
      }
    },
    error:(err)=>{
    }
  })

}
}
