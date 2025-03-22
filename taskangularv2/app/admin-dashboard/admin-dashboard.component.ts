import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { Project } from '../project';

interface Projectinfo {
  id: string;
  name: string;
  pm: string;
}
interface UserInfo {
  name: string;
  email: string;
  role: string;
  id: string;
}

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {
  projects: Projectinfo [] = [];
  Users:UserInfo[]=[];

  constructor(private router:Router, private srv: DbAccessService) { 
    
   
  }
  NavigateToaddNewUser(){
    this.router.navigate(['addnewuser']);
  }
  NavigateToAddNewProject(){
    this.router.navigate(['addnewproject']);
  }
  ngOnInit(): void {
    this.getProjects();
    this.getUsers();
  }
  getUsers():void{
    this.srv.GetAllUsers().subscribe({
      next:(res:UserInfo[])=>{
        this.Users=res;
      },
      error:(err)=>{
        console.log(err);
      }
    });
  }
  
  getProjects(): void {
    this.srv.GetAllProjects().subscribe({
      next: (res: Projectinfo[]) => {
        this.projects=res;
      },
      error: (err) => {
        console.error('Error fetching projects:', err);
      }
    });
  }


}
