import { Component, OnInit } from '@angular/core';
import { Aduser } from '../aduser';
import { MydataService } from '../mydata.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-ad-user-list',
  templateUrl: './ad-user-list.component.html',
  styleUrls: ['./ad-user-list.component.css']
})
export class AdUserListComponent implements OnInit {
  userList:Aduser[]=[];
  constructor(private srv:MydataService/*,private http:HttpClient*/)
  {

  }
  ngOnInit(): void {      
    this.RefreshView();
  }

  RefreshView():void
  {
    this.srv.getUserList()
      .subscribe({
            next:(res)=>{
              this.userList=<Aduser[]>res;
            },
            error:(err)=>{
              console.log(err);
            }
      });
  }  
DeleteUser(id:string):void{
    this.srv.deleteUser(id).subscribe({
      next:()=>{
        //Refresh the View.
          this.RefreshView();
      },
      error:(err)=>{
      }
    })
  }
}
