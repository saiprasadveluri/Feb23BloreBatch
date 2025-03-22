import { Component, OnInit } from '@angular/core';
import { DBAccessService } from '../dbaccess.service';

@Component({
  selector: 'app-view-projects',
  templateUrl: './view-projects.component.html',
  styleUrls: ['./view-projects.component.css']
})
export class ViewProjectsComponent implements OnInit {
  projects: any[] = []; // Array to store users fetched from db.json

  constructor(private adminService: DBAccessService) {}

  ngOnInit(): void {
    this.loadProjects(); // Fetch users when the component initializes
  }

  // Fetch users from db.json
  loadProjects(): void {
    this.adminService.GetAllProjects().subscribe(data => {
      this.projects = data;
    });
  }

  // Delete a user by ID
  deleteProject(projectId: string): void {
    if (confirm('Are you sure you want to delete this project?')) {
      this.adminService.deleteProject(projectId).subscribe(() => {
        alert('Project deleted successfully');
        this.loadProjects(); // Reload the project list after deletion
      });
    }
  }
}