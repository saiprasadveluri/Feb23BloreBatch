import { Component } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import { DBAccessService } from '../dbaccess.service';
import { UserInfo } from '../user-info';
import { Router } from '@angular/router';
@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent {
frmGroup:FormGroup;
  constructor(private fb:FormBuilder,private srv:DBAccessService,private router:Router) {
    this.frmGroup = fb.group({
      uemail: new FormControl('', [Validators.required]),
      upassword: new FormControl('', [Validators.required])
    });
  }
  OnLoginClick()
  {
    var uemail = this.frmGroup.controls['uemail'].value;
    var upassword = this.frmGroup.controls['upassword'].value;
    //console.log(uemail);
    this.srv.GetAllUsers().subscribe({
      next:(res)=>{
        var AllUsers=<UserInfo[]>res;
        console.log(AllUsers);
        var FilterObj = AllUsers.find((u)=>(u.email==uemail && u.password==upassword));
        if(FilterObj!=undefined)
        {
          console.log("Login Success");
          //Store the user data into storage
          sessionStorage.setItem('LoggedInUserData',JSON.stringify(FilterObj));
          switch(FilterObj.role.toUpperCase())
          {
            case 'ADMIN':
              {
              this.router.navigate(['admindashboard']);
              break;
              }
              case 'PM':
                {
                this.router.navigate(['pmdashboard']);
                break;
                }
                case 'MEMBER':
                  {
                  this.router.navigate(['memberdashboard']);
                  break;
                  }
          }
        }
      },
  error:(err)=>{
    
  }
    });
  }
}
