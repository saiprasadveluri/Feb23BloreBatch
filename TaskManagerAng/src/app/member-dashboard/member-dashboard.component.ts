import { Component, OnInit } from '@angular/core';
import { UserInfo } from '../user-info';
import { Task } from '../task';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-member-dashboard',
  templateUrl: './member-dashboard.component.html',
  styleUrls: ['./member-dashboard.component.css']
})
export class MemberDashboardComponent implements OnInit {
  constructor(private srv:DbAccessService,private router:Router){}
 LoggedInUser:UserInfo=JSON.parse(sessionStorage.getItem('LoggedinUserData')as string);
 taskList:Task[]=[];
 user:string=''

  ngOnInit(): void {
    this.user=this.LoggedInUser.role;
    
    
  }

}
