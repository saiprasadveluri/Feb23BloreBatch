import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-assign-members',
  templateUrl: './assign-members.component.html',
  styleUrls: ['./assign-members.component.css']
})
export class AssignMembersComponent implements OnInit {
  availableMembers: UserInfo[] = []; // List of available members (Developers and QA)
  selectedMembers: string[] = []; // IDs of selected members (string to match db.json)
  projectId = '1'; // Replace with the selected project's ID (string to match db.json)

  constructor(private dbService: DbAccessService) {}

  ngOnInit() {
    // Fetch all available members (Developers and QA)
    this.dbService.GetAllUsers().subscribe(
      (data: UserInfo[]) => {
        this.availableMembers = data.filter(user => user.role === 'DEVELOPER' || user.role === 'QA');
      },
      (error) => {
        console.error('Error fetching available members:', error);
        alert('Failed to load available members. Please try again later.');
      }
    );
  }

  AssignMembers() {
    if (this.selectedMembers.length === 0) {
      alert('Please select at least one member to assign.');
      return;
    }

    // Assign selected members to the project
    this.dbService.AssignMembersToProject(this.projectId, this.selectedMembers).subscribe(
      () => {
        alert('Members assigned successfully!');
        this.selectedMembers = []; // Clear the selection after successful assignment
      },
      (error) => {
        console.error('Error assigning members:', error);
        alert('Failed to assign members. Please try again later.');
      }
    );
  }
}