import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { Projectinfo } from '../projectinfo';
@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {
  projects: Projectinfo[] = [];
    
  constructor(private srv:DbAccessService,private router:Router) {

    }
  
    ngOnInit(): void {
      this.loadusers();

    }
    loadusers():void{
      this.srv.GetAllProjects().subscribe({
        next: (res) => {
          this.projects = res as Projectinfo[];
          console.log('Projects fetched successfully:', this.projects);
          
        },
        error: (err) => {
          console.error('Error fetching users:', err);
        }
      });
    }

  Gotoadduser()
  {
    this.router.navigate(['addnewuser']);
  }
  Gotoaddproject()
  {
    this.router.navigate(['addnewproject'])
  }

  

}
