import { Component } from '@angular/core';
import { Projectinfo } from '../projectinfo';
import { DbAccessService } from '../db-access.service';

@Component({
  selector: 'app-get-projects',
  templateUrl: './get-projects.component.html',
  styleUrls: ['./get-projects.component.css']
})
export class GetProjectsComponent {


  projects: Projectinfo[] = [];
  
    constructor(private srv:DbAccessService) {

    }
  
    ngOnInit(): void {
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
}


