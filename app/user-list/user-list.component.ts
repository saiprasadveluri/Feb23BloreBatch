import { Component , OnInit} from '@angular/core';
import { UserInfo } from '../user-info';
import { DbAccessService } from '../db-access.service';
import { Project } from '../project';

@Component({
  selector: 'app-user-list',
  standalone: false,
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent implements OnInit{
  userList:UserInfo[]=[];
  constructor(private srv:DbAccessService){

  }
  ngOnInit(): void {
    this.RefreshView();
  }

  RefreshView():void{
    this.srv.GetAllUsers().subscribe({
      next:(res)=>{
        this.userList=<UserInfo[]>res;
      },
      error:(err)=>{
        console.log(err);
      }
    });
  }

  

}
