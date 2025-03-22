import { Component, OnInit } from '@angular/core';
import { DBAccessService } from '../dbaccess.service';

@Component({
  selector: 'app-view-users',
  templateUrl: './view-users.component.html',
  styleUrls: ['./view-users.component.css']
})
export class ViewUsersComponent implements OnInit {
  users: any[] = []; // Array to store users fetched from db.json

  constructor(private adminService: DBAccessService) {}

  ngOnInit(): void {
    this.loadUsers(); // Fetch users when the component initializes
  }

  // Fetch users from db.json
  loadUsers(): void {
    this.adminService.GetAllUsers().subscribe(data => {
      this.users = data;
    });
  }

  // Delete a user by ID
  deleteUser(userId: string): void {
    if (confirm('Are you sure you want to delete this user?')) {
      this.adminService.deleteUser(userId).subscribe(() => {
        alert('User deleted successfully');
        this.loadUsers(); // Reload the user list after deletion
      });
    }
  }
}