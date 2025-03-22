// import { Component } from '@angular/core';

// @Component({
//   selector: 'app-member-dashboard',
//   templateUrl: './member-dashboard.component.html',
//   styleUrls: ['./member-dashboard.component.css']
// })
// export class MemberDashboardComponent {
  

// }
import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';

interface Tasks {
  id?: string;
  projid: string;
  title: string;
  description: string;
  tasktype: string;
  assignedto: string;
  status: string;
}

@Component({
  selector: 'app-member-dashboard',
  templateUrl: './member-dashboard.component.html',
  styleUrls: ['./member-dashboard.component.css']
})
export class MemberDashboardComponent implements OnInit {
  tasks: Tasks[] = []; 
  userId: string | null = null; 

  constructor(private srv: DbAccessService, private router: Router) {}

  ngOnInit(): void {
    this.getLoggedInUserId(); 
    this.getTasks(); 
  }

 
  getLoggedInUserId(): void {
    const sessionData = sessionStorage.getItem('LoggedinUserData');
    if (sessionData) {
      const userData = JSON.parse(sessionData);
      this.userId = userData?.id || null;
      console.log('Logged-in User ID:', this.userId);
    } else {
      console.warn('No valid session data found for the logged-in user');
    }
  }

  getTasks(): void {
    if (this.userId) {
      this.srv.GetTasks().subscribe({
        next: (res: Tasks[]) => {
          this.tasks = res.filter(task => task.assignedto === this.userId);
          console.log('Tasks assigned to the user:', this.tasks);
        },
        error: (err) => {
          console.error('Error fetching tasks:', err);
        }
      });
    } else {
      console.warn('User ID is not available. Cannot fetch tasks.');
    }
  }
  addComment(task: any): void {
    console.log('Comment button clicked for task:', task);
    // Add logic to handle comments here
  }
}