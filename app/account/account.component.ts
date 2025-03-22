import { Component } from '@angular/core';
 import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
 import { DbAccessService } from '../db-access.service';
 import { UserInfo } from '../user-info';
 import { Router } from '@angular/router';
 import { ProjectInfo } from '../project-info';
  
 @Component({
   selector: 'app-account',
   templateUrl: './account.component.html',
   styleUrls: ['./account.component.css']
 })
 export class AccountComponent {
 frmGroup:FormGroup;
 constructor(private fb:FormBuilder,private srv:DbAccessService,private router:Router)
 {
   this.frmGroup = this.fb.group({
     uemail: new FormControl('', [Validators.required]),
     upassword: new FormControl('', [Validators.required]),
   });
 }
 OnLoginClick()
 {
    var uemail=this.frmGroup.controls['uemail'].value;
    var upassword=this.frmGroup.controls['upassword'].value;
    console.log(uemail);
    this.srv.getAllUsers().subscribe({
      next:(res: any)=>{
       var AllUsers=<UserInfo[]>res;
       console.log(AllUsers);
       var FilterObj=AllUsers.find((u)=>(u.email==uemail && u.password));
       if(FilterObj!=undefined)
       {
         console.log(FilterObj.role);
         // store the userdata into storage
         sessionStorage.setItem('LoggedinUserData',JSON.stringify(FilterObj));
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
      error:(error)=>{
  
      }
    });
  
 }
  
 }
  