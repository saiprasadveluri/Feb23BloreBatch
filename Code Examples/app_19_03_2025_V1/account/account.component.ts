import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MydataService } from '../mydata.service';
import { Aduser } from '../aduser';
import { Router } from '@angular/router';
import { MychangeemitterService } from '../mychangeemitter.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit{
email:string='';
password:string='';
msg:string='';

constructor(private srv:MydataService,private route:Router,private src:MychangeemitterService)
{
  this.src.EmitEvent(false);
}
  ngOnInit(): void {
    
  }
OnLogin()
{
  this.srv.getUserList().subscribe(
    {
      next:(res)=>{
        var AllUsers:Aduser[]=<Aduser[]>res;
        var curUsers=AllUsers.filter(u=>{return (u.email==this.email && u.password==this.password)});
        if(curUsers.length>0)
        {
            var LoggedInUser=curUsers[0];
            sessionStorage.setItem("LoggedInUser",JSON.stringify(LoggedInUser));
            
            this.src.EmitEvent(true);

            switch(LoggedInUser.role.toUpperCase())
            {             
              case 'ADMIN':
                  this.route.navigate(['admindashboard']);
                break;
                case 'OWNER':
                  this.route.navigate(['ownerdashboard']);
                  break;
            }            
            this.msg="Login success";
        }  
        else{
          this.msg="Login failed";
        }      
      },
      error:(err)=>{this.msg="Error In Fetching data"}
    }
  );
}
}
