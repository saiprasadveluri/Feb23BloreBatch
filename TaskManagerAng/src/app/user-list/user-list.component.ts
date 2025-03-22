import { Component ,OnInit} from '@angular/core';
import { UserInfo } from '../user-info';
import { DbAccessService } from '../db-access.service';
@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  userList:UserInfo[]=[];

  constructor(private srv:DbAccessService){

  }

  ngOnInit():void{
    this.RefreshView();
  }

  RefreshView():void{
    this.srv.GetAllUsers().subscribe({
      next: (res) => {
        this.userList = <UserInfo[]>res;
      },
      error: (err) => {
        console.log('Error fetching user list:', err);
      }
    });
  }

  DeleteUser(id:string):void{
    this.srv.DeleteUser(id).subscribe({
      next: () => {
        this.RefreshView();
      },
      error: (err) => {
        console.log('Error deleting user:', err);
      }
    });
  }
}