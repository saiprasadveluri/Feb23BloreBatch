import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-users',
  templateUrl: './view-users.component.html',
  styleUrls: ['./view-users.component.css']
})
export class ViewUsersComponent implements OnInit{
  userList:UserInfo[]=[];
  constructor(private srv:DbAccessService,private router:Router)
  {
  
  }

  goToPMDashboard(): void {
    this.router.navigate(['admindashboard']); // Change to your actual PM dashboard route
  }
  ngOnInit(): void {      
    this.RefreshView();
  }

  RefreshView():void
  {
    this.srv.GetAllUsers()
      .subscribe({
            next:(res)=>{
              this.userList=<UserInfo[]>res;
            },
            error:(err)=>{
              console.log(err);
            }
      });
  }  
// DeleteUser(id:string):void{
//     this.srv.deleteUser(id).subscribe({
//       next:()=>{
//         //Refresh the View.
//           this.RefreshView();
//       },
//       error:(err)=>{
//       }
//     })
//   }
}





