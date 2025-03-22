import { Component } from '@angular/core';
import { DbAccessServiceService } from '../db-access-service.service';
import { Project } from '../project';

@Component({
  selector: 'app-project-list',
  standalone: false,
  templateUrl: './project-list.component.html',
  styleUrl: './project-list.component.css'
})
export class ProjectListComponent {
  projects: Project[] = []; // Array to store project data
  isLoading: boolean = true; // Show loader while fetching data

  constructor(private dbService: DbAccessServiceService) {}

  ngOnInit(): void {
    this.fetchProjects();
  }

  fetchProjects(): void {
    this.dbService.getProjects().subscribe({
      next: (response: any[]) => {
        this.projects = response; // Assign fetched projects to the array
        this.isLoading = false; // Stop loader
      },
      error: (error) => {
        console.error('Error fetching projects:', error);
        this.isLoading = false; // Stop loader in case of error
      }
    });
  }
}
