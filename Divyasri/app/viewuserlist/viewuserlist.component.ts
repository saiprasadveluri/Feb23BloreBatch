import { Component } from '@angular/core';
import { UserInfo } from '../user-info';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-viewuserlist',
  templateUrl: './viewuserlist.component.html',
  styleUrls: ['./viewuserlist.component.css']
})
export class ViewuserlistComponent {

  id:string='';
  usrList:UserInfo []=[];
  constructor(private srv:DbAccessService,private router:Router)
  {

  }

ngOnInit(): void {      
  this.RefreshView();
}

RefreshView():void
{
  this.srv.GetAllUsers().subscribe({
          next:(res)=>{
           var AlluserList:UserInfo[]=<UserInfo[]>res;
           this.usrList=AlluserList;
           console.log("updated user list:",this.usrList);
          },
          error:(err)=>{
            console.log(err);
          }
    });
} 


}

