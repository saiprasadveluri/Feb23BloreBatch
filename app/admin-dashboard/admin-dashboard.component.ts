import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Project } from '../project';
import { DbAccessService } from '../db-access.service';
import { FormBuilder } from '@angular/forms';
import { UserInfo } from '../user-info';


@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {
  projects: Project[] = [];
  users:UserInfo[] = [];
  constructor(private fb:FormBuilder, private srv:DbAccessService, private router:Router) {
      
    }
    
  RefreshView():void
  {
    this.srv.GetAllUsers()
      .subscribe({
            next:(res)=>{
              this.users=<UserInfo[]>res;
            },
            error:(err)=>{
              console.log(err);
            }
      });
  }  

  NavigateToAddNewUser()
  {
    this.router.navigate(['addnewuser']);
  }

  NavigateToNewProj()
  {
    this.router.navigate(['addnewproject']);
  }

  DeleteUser(id:string):void{
    this.srv.DeleteUser(id).subscribe({
      next:()=>{
          this.RefreshView();
      },
      error:(err)=>{
      }
    });
  }

  ngOnInit(): void {
    this.srv.GetAllUsers().subscribe({
      next: (res) => {
        this.users = <UserInfo[]>res;
      }
    });
  
      this.srv.GetAllProjects().subscribe({
        next: (res) => {
          this.projects = <Project[]>res;
        }
      });
    }
}
