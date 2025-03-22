import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfo } from '../user-info';
import { ProjInfo } from '../proj-info';
import { DbAccessService } from '../db-access.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {
   users:UserInfo[]=[];
    projects: ProjInfo[] = [];
  
constructor(private router:Router,private dataservice:DbAccessService)
{
  
}
ngOnInit(): void{
  this.dataservice.GetAllUsers().subscribe((data: any[]) => {
    this.users = data.filter((user: { name: any; }) => user.name);
  });             
  this.dataservice.getAllProjects().subscribe((data: any[]) => {
    this.projects = data.filter((project: { name: any; }) => project.name);
  });
}
NavigateToAddNewUser()
{
  this.router.navigate(['addnewuser']);
}
NavigateToAddNewProject()
{
  this.router.navigate(['addnewproject']);
}

}

