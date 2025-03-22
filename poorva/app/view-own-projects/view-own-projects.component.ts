import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-view-own-projects',
  templateUrl: './view-own-projects.component.html',
  styleUrls: ['./view-own-projects.component.css']
})
export class ViewOwnProjectsComponent implements OnInit {
  ownProjects: any[] = [];
  managerId: string | null = localStorage.getItem('userId');
  constructor(private dbAccessService: DbAccessService, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    if (this.managerId) {
      this.dbAccessService.GetOwnProjects(this.managerId).subscribe(
        (data) => {
          console.log('Received projects:', data); // Check the console for this log
          this.ownProjects = data;
          this.cdr.detectChanges(); // Force change detection
        },
        (error) => {
          console.error('Error fetching projects:', error); // Log any errors
        }
      );
    } else {
      console.warn('Manager ID is null');
    }
  }
}