import { Component, OnInit } from '@angular/core';
import { DbAccessServiceService } from '../db-access-service.service';
import { UserInfo } from '../userinfo';

@Component({
  selector: 'app-user-list',
  standalone: false,
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent implements OnInit{
  users: UserInfo[] = []; // Array to hold user data
  isLoading: boolean = true; // Show loader during API call

  constructor(private dbService: DbAccessServiceService) {}

  ngOnInit(): void {
    this.fetchUsers();
  }

  fetchUsers(): void {
    this.dbService.getUsers().subscribe({
      next: (response: UserInfo[]) => {
        this.users = response; // Assign fetched users to the array
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error fetching users:', error);
        this.isLoading = false;
      }
    });
  }

  }
  
  
    
  
  

