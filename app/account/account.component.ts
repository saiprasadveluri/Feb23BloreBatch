import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TmaccessService } from '../tmaccess.service';
import { Userinfo } from '../userinfo';
import { Router } from '@angular/router';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent {
    frmGroup:FormGroup;
    
    constructor(private fb: FormBuilder,private srv:TmaccessService,private router:Router) {
        this.frmGroup = this.fb.group({
           uemail:new FormControl('',[Validators.required]),
           upassword:new FormControl('',[Validators.required])
        });
    }

    onLoginClick(){
      var uemail=this.frmGroup.controls['uemail'].value;
      var upassword=this.frmGroup.controls['upassword'].value;
        console.log(uemail);
        this.srv.getallusers().subscribe({
          next:(res)=>{
            var allusers=<Userinfo[]>res;
           // console.log(allusers);
            var filtereduser=allusers.find((u)=>(
               u.email==uemail && u.password==upassword
            )
            );
            if(filtereduser!=undefined){
              console.log("Login Success");
              //store userdata into session storage
              sessionStorage.setItem('LoggedinUser',JSON.stringify(filtereduser));
              switch(filtereduser.role.toUpperCase()){
                case "ADMIN":{this.router.navigate(['admindashboard']);break;}
                case "PM":{this.router.navigate(['pmdashboard']);break;}
                case "DEV":{this.router.navigate(['memberdashboard']);break;}
                case "QA":{this.router.navigate(['memberdashboard']);break;}
              }
              
            }
            else{
              console.log("Login Failed");
            }
          },
          error:(err)=>{
            
          }
        });
    }
  }
