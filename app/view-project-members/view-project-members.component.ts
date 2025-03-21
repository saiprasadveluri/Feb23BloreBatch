import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';

@Component({
  selector: 'app-view-project-members',
  templateUrl: './view-project-members.component.html',
  styleUrls: ['./view-project-members.component.css']
})
export class ViewProjectMembersComponent implements OnInit {
  members: any[] = []; // Holds the list of project members
  selectedProjectId: string = ''; // The project ID for which members are being fetched

  constructor(private dbService: DbAccessService) {}

  ngOnInit(): void {
    // Optionally, you can set a default project ID here
    this.selectedProjectId = '1'; // Replace with the actual project ID
    this.GetMembersByProject(this.selectedProjectId);
  }

  GetMembersByProject(projid: string): void {
    this.dbService.GetMembersByProject(projid).subscribe({
      next: (data: any[]) => {
        this.members = data; // Assign the fetched members to the `members` array
      },
      error: (error) => {
        console.error('Error fetching project members:', error);
      }
    });
  }
}