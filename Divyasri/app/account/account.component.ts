import { Component } from '@angular/core';
import { FormGroup,FormBuilder,FormControl,Validators} from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';
import { Router } from '@angular/router';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent {
  frmGroup:FormGroup;
  constructor(private fb: FormBuilder,private srv:DbAccessService,private router:Router)
  {
    this.frmGroup=this.fb.group({
      uemail:new FormControl('',[Validators.required]),
      upassword:new FormControl('',[Validators.required])
    })
    
  }
  OnLoginClick() {
 
    var uemail=this.frmGroup.controls['uemail'].value;
    var upassword=this.frmGroup.controls['upassword'].value;
    console.log(this.frmGroup);
    this.srv.GetAllUsers().subscribe({
      next: (res) => {
      
          var AllUsers=<UserInfo[]>res;
          console.log(AllUsers);
          var filterObj=AllUsers.find((u)=>(u.email==uemail && u.password==upassword));
          if(filterObj!=undefined)
          {
            console.log("Success");
            //store the user data into sessions
            sessionStorage.setItem('LoggedinUserData',JSON.stringify(filterObj));
            switch(filterObj.role.toUpperCase())
            {
              case 'ADMIN':{
                this.router.navigate(['admindashboard']);
                break;

              }
              case 'PM': {
                this.router.navigate(['pm-dashboard']);
                break;
              }
              case 'DEVELOPER':
              {
                this.router.navigate(['user-dashboard']);
                break;
              }
              case 'QA':
                {
                  this.router.navigate(['user-dashboard']);
                  break;
                }
            }
         

          }
      },
      error: (err) => {
        
      }
    })
  }
 
}
