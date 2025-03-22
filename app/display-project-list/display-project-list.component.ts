import { Component, OnInit } from '@angular/core';
import { DisplayProjectList } from '../display-project-list';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-display-project-list',
  templateUrl: './display-project-list.component.html',
  styleUrls: ['./display-project-list.component.css']
})
export class DisplayProjectListComponent implements OnInit {
  projectList: DisplayProjectList[] = []; // Define projectList as an array

  constructor(private srv: DbAccessService, private router: Router) {}

  ngOnInit(): void {
    this.RefreshView();
  }

  RefreshView(): void {
    this.srv.DisplayProject().subscribe({
      next: (res) => {
        this.projectList = res; // Assign the response to projectList
        console.log('Updated project list:', this.projectList);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  AssignTo(projectId: string | undefined): void {
    this.router.navigate(['assignto', projectId]);
  }
}