import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Project } from '../project';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';

@Component({
  selector: 'app-pm-dashboard',
  templateUrl: './pm-dashboard.component.html',
  styleUrls: ['./pm-dashboard.component.css']
})
export class PmDashboardComponent {
  projects: Project[] = [];
  loggedInUserId: string | null = null;

  constructor(private fb: FormBuilder, private srv: DbAccessService, private router: Router) {
    const loggedInUser = JSON.parse(sessionStorage.getItem('LoggedInUser') || '{}');
    this.loggedInUserId = loggedInUser.id || null;
  }

  ngOnInit(): void {
    if (this.loggedInUserId) {
      this.srv.GetProjectById(this.loggedInUserId).subscribe({
        next: (res) => {
          this.projects = <Project[]>res;
        },
        error: (err) => {
          console.error('Error fetching projects:', err);
        }
      });
    }
  }

  NavigateToMembers(id:string): void {
    sessionStorage.setItem('Project',id);
    this.router.navigate(['projectmembers']);
  }

  NavigateToTask(id:string):void{
    sessionStorage.setItem('Project',id);
    this.router.navigate(['adtask']);
  }
}
