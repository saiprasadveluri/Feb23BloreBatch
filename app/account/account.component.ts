import { Component } from '@angular/core';
import{DbAccessService} from '../db-access.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Userinfo } from '../userinfo';
import { Router } from '@angular/router';
@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent {
frmGroup: FormGroup;
  constructor(private fb: FormBuilder,private srv:DbAccessService,private route:Router){
    this.frmGroup = this.fb.group({
     uemail:new FormControl('',[Validators.required]),
     upassword:new FormControl('',[Validators.required]),
    });
  }
  onloginclick(){
   var uemail=this.frmGroup.controls['uemail'].value;
   var upassword=this.frmGroup.controls['upassword'].value;
   console.log(uemail);
   this.srv.GetAllUsers().subscribe({
    next: (res) => {
        var Allusers=<Userinfo[]>res;
        
        var Filterobj=Allusers.find(x=>x.email==uemail && x.password==upassword);
        if(Filterobj!=undefined)
        {
          console.log('Login Success');
          //store the user data into storage
          sessionStorage.setItem('LoggedinUserData',JSON.stringify(Filterobj));
          switch(Filterobj.role.toUpperCase()){
            case 'ADMIN':
              this.route.navigate(['/admindashboard']);
              break;
            case 'PM':
              this.route.navigate(['/pmdashboard']);
              break;
            case 'DEV':
              this.route.navigate(['/memberdashboard']);
              break;
            case 'QA':
              this.route.navigate(['/memberdashboard']);
              break;
          }
        }
    },
    error: (err) => {
        console.log(err);
    }
   })
  }
}
