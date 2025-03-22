import { Component } from '@angular/core';
import { Task } from '../task';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Userinfo } from '../userinfo';

@Component({
  selector: 'app-member-dashboard',
  templateUrl: './member-dashboard.component.html',
  styleUrls: ['./member-dashboard.component.css']
})
export class MemberDashboardComponent {
  tasks:Task[] = [];
  loggedInUserId: string = '';
  loggedInuserrole:string='';
  constructor(private srv: DbAccessService,private route:Router) {}

  ngOnInit(): void {
    // Retrieve logged-in user data from sessionStorage
    const LoggedinUserData: Userinfo = JSON.parse(
      sessionStorage.getItem('LoggedinUserData') as string
    );
    this.loggedInUserId = LoggedinUserData.id || '';
    this.loggedInuserrole=LoggedinUserData.role;

    // Fetch all tasks and filter by the logged-in user's ID
    this.srv.GetAllTasks().subscribe({
      next: (res) => {
        const allTasks = res as Task[];
        this.tasks = allTasks.filter((task) => task.assignedto === this.loggedInUserId);
        console.log('Filtered tasks for member:', this.tasks);
      },
      error: (err) => {
        console.error('Error fetching tasks:', err);
      }
    });

    if(this.loggedInuserrole=='DEV')
      {
        this.developerplace();
      }
      else{
        this.QAplace();
      }
  }
  developerplace():void
  {

  }
  QAplace():void{

  }
  addComment(taskid: string): void {
    this.route.navigate(['addcomment'], {
      queryParams: { taskid: taskid, commentedby: this.loggedInUserId }
    });
  }

}
