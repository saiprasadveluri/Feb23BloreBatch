import { Component, OnInit } from '@angular/core';
 import { DbAccessService } from '../db-access.service';
 import { UserInfo } from '../user-info';
  
 @Component({
   selector: 'app-view-users',
   templateUrl: './view-users.component.html',
   styleUrls: ['./view-users.component.css']
 })
 export class ViewUsersComponent implements OnInit {
   users: UserInfo[] = [];
  
   constructor(private dbService: DbAccessService) {}
  
   ngOnInit() {
     this.dbService.getAllUsers().subscribe((data: UserInfo[]) => {
       this.users = data;
     });
   }
 }