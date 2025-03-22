import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { Userinfo } from '../userinfo';

@Component({
  selector: 'app-get-users',
  templateUrl: './get-users.component.html',
  styleUrls: ['./get-users.component.css']
})
export class GetUsersComponent implements OnInit {
  users: Userinfo[] = [];

  constructor(private srv: DbAccessService) {}

  ngOnInit(): void {
  this.Loadusers();
  }
  Loadusers():void{
    this.srv.GetAllUsers().subscribe({
      next: (res) => {
        this.users = res as Userinfo[];
        console.log('Users fetched successfully:', this.users);
      },
      error: (err) => {
        console.error('Error fetching users:', err);
      }
    });
  }
  deleteuser(userid:string):void{
    this.srv.Deleteuser(userid).subscribe({
      next:()=>
      {
        console.log("Dlelted user sucess");
        this.Loadusers();
      },
      error:(err)=>
      {
        console.error("Error deleting user");
      }
    });
  }
}