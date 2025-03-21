import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-remove-members',
  templateUrl: './remove-members.component.html',
  styleUrls: ['./remove-members.component.css']
})
export class RemoveMembersComponent implements OnInit {
  members: { id: string; user: UserInfo }[] = []; // List of members with their ProjectMember ID and UserInfo
  projId = '1'; // Replace with the selected project's ID

  constructor(private dbService: DbAccessService) {}

  ngOnInit() {
    // Fetch members assigned to the project
    this.dbService.GetMembersByProject(this.projId).subscribe((data: UserInfo[]) => {
      this.members = data.map((user, index) => ({
        id: (index + 1).toString(), // Use the ProjectMember ID
        user
      }));
    });
  }

  RemoveMember(projectMemberId: string) {
    this.dbService.RemoveMemberFromProject(projectMemberId).subscribe(() => {
      alert('Member removed successfully!');
      // Update the members list after removal
      this.members = this.members.filter(member => member.id !== projectMemberId);
    });
  }
}