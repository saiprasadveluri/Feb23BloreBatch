import { Component, OnInit } from '@angular/core';
import { Taskinfo } from '../taskinfo';
import { DbAccessService } from '../db-access.service';
import { Userinfo } from '../userinfo';
import { Router } from '@angular/router';

@Component({
  selector: 'app-member-dashboard',
  templateUrl: './member-dashboard.component.html',
  styleUrls: ['./member-dashboard.component.css']
})
export class MemberDashboardComponent implements OnInit {
  tasks: Taskinfo[] = [];
  loggedInUserId: string = '';
  loggedInuserrole: string = '';

  constructor(private srv: DbAccessService, private route: Router) {}

  ngOnInit(): void {
    // Retrieve logged-in user data from sessionStorage
    const LoggedinUserData: Userinfo = JSON.parse(
      sessionStorage.getItem('LoggedinUserData') as string
    );
    this.loggedInUserId = LoggedinUserData.id || '';
    this.loggedInuserrole = LoggedinUserData.role;

    // Fetch all tasks and filter by the logged-in user's ID
    this.srv.GetAllTasks().subscribe({
      next: (res) => {
        const allTasks = res as Taskinfo[];
        this.tasks = allTasks.filter((task) => task.assignedto === this.loggedInUserId);
        console.log('Filtered tasks for member:', this.tasks);
      },
      error: (err) => {
        console.error('Error fetching tasks:', err);
      }
    });
  }

  navigateToDeveloperPlace(): void {
    this.route.navigate(['developerplace']);
  }

  navigateToQaPlace(): void {
    this.route.navigate(['qaplace']);
  }

  addComment(taskid: string): void {
    this.route.navigate(['addcomment'], {
      queryParams: { taskid: taskid, commentedby: this.loggedInUserId }
    });
  }
}