import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';

@Component({
  selector: 'app-view-users',
  templateUrl: './view-users.component.html',
  styleUrls: ['./view-users.component.css']
})
export class ViewUsersComponent implements OnInit{
  users:any[]=[];

  constructor(private dbAccessService:DbAccessService){

  }
  ngOnInit(): void {
    this.dbAccessService.GetAllUsers().subscribe((data)=>{
        this.users=data;
    });
  }

}
