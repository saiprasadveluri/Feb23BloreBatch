import { Component } from '@angular/core';
import { Task } from '../task';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Userinfo } from '../userinfo';
import { Projectmember } from '../projectmember';
import { Project } from '../project';

@Component({
  selector: 'app-member-dashboard',
  templateUrl: './member-dashboard.component.html',
  styleUrls: ['./member-dashboard.component.css']
})
export class MemberDashboardComponent {
  tasks:Task[] = [];
  prjmems:Projectmember[]=[];
  projects:Project[]=[];
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
  getProjectName(pid:string):string{
    var project=this.projects.find((p)=>p.id==pid);
    return project ? project.name : 'Name NotFound';

}

}
