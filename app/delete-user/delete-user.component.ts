import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-delete-user',
  templateUrl: './delete-user.component.html',
  styleUrls: ['./delete-user.component.css']
})
export class DeleteUserComponent implements OnInit {
  users: UserInfo[] = [];

  constructor(private dbService: DbAccessService) {}

  ngOnInit() {
    // Fetch all users
    this.dbService.GetAllUsers().subscribe((data: UserInfo[]) => {
      this.users = data;
    });
  }

  DeleteUser(userId: number | undefined) {
    if (userId === undefined) {
      alert('Invalid user ID');
      return;
    }
    this.dbService.DeleteUser(userId).subscribe(() => {
      alert('User deleted successfully');
      // Refresh the user list after deletion
      this.users = this.users.filter(user => user.id !== userId);
    });
  }
}