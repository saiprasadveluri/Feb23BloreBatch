import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfo } from '../user-info';
import { ProjInfo } from '../proj-info';
import { TaskInfo } from '../task-info'; // Import TaskInfo
import { DbAccessService } from '../db-access.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {
   users: UserInfo[] = [];
   projects: ProjInfo[] = [];
   tasks: TaskInfo[] = []; // Add tasks array
  
constructor(private router: Router, private dataservice: DbAccessService) { }

ngOnInit(): void {
  this.dataservice.GetAllUsers().subscribe((data: any[]) => {
    this.users = data.filter((user: { name: any; }) => user.name);
  });             
  this.dataservice.getAllProjects().subscribe((data: any[]) => {
    this.projects = data.filter((project: { name: any; }) => project.name);
  });
  this.RefreshView(); 
}

RefreshView() {
  this.dataservice.getAllTasks().subscribe((data: any[]) => {
    this.tasks = data.filter((task: { title: any; status: any; }) => task.title && task.status); // Ensure task has title and status
  });
}

NavigateToAddNewUser() {
  this.router.navigate(['addnewuser']);
}

NavigateToAddNewProject() {
  this.router.navigate(['addnewproject']);
}

NavigateToAddNewTask(project: ProjInfo) { // Accept project parameter
  this.router.navigate(['addnewtask', project.id]); // Navigate to add new task with project id
  console.log('Navigating to add new task for project:', project);
}

}

