import { Component } from '@angular/core';
import{DbAccessService} from '../db-access.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserInfo } from '../user-info';
import { Router } from '@angular/router';
@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent {
frmGroup: FormGroup;
  constructor(private fb: FormBuilder,private srv:DbAccessService,private route:Router) {
    this.frmGroup = this.fb.group({
     uemail:new FormControl('',[Validators.required]),
     upassword:new FormControl('',[Validators.required]),
    });
  }
  OnLoginClick(){
   var uemail=this.frmGroup.controls['uemail'].value;
   var upassword=this.frmGroup.controls['upassword'].value;
   this.srv.GetAllUsers().subscribe({
    next: (res) => {
        var Allusers=<UserInfo[]>res;
        var Filterobj=Allusers.find(x=>x.email==uemail && x.password==upassword);
        if(Filterobj!=undefined)
        {
          sessionStorage.setItem('LoggedinUserData',JSON.stringify(Filterobj));
          switch (Filterobj.role.trim().toUpperCase()){
            case 'ADMIN':             
              this.route.navigate(['admindashboard']);
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
 
 