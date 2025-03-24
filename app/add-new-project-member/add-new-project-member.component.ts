import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { ProjectManager } from '../projectmanager';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-add-new-project-member',
  templateUrl: './add-new-project-member.component.html',
  styleUrls: ['./add-new-project-member.component.css']
})
export class AddNewProjectMemberComponent implements OnInit {
  projectId: string = '';
  members: ProjectManager[] = [];
  users: UserInfo[] = [];
  errorMessage: string = '';

  constructor(private srv: DbAccessService, private route: ActivatedRoute) {
    this.projectId = this.route.snapshot.paramMap.get('projectId') || '';
  }

  ngOnInit(): void {
    this.loadMembers();
    this.loadUsers();
  }

  loadMembers(): void {
    this.srv.GetProjectMembers(this.projectId).subscribe({
      next: (members) => {
        this.members = members;
      },
      error: (err) => {
        console.error('Failed to fetch project members', err);
        this.errorMessage = 'Failed to fetch project members';
      }
    });
  }

  loadUsers(): void {
    this.srv.GetAllUsers().subscribe({
      next: (users) => {
        this.users = users.filter((user: UserInfo) => user.role === 'DEVELOPER' || user.role === 'QA');
      },
      error: (err) => {
        console.error('Failed to fetch users', err);
        this.errorMessage = 'Failed to fetch users';
      }
    });
  }

  assignMember(userId: string): void {
    const newMember: ProjectManager = {
      id: '',
      projId: this.projectId,
      memid: userId
    };
    this.srv.AddProjectMember(newMember).subscribe({
      next: () => {
        this.loadMembers();
      },
      error: (err) => {
        console.error('Failed to assign member', err);
        this.errorMessage = 'Failed to assign member';
      }
    });
  }

  removeMember(memberId: string): void {
    console.log(`Removing member with ID: ${memberId}`);
    this.srv.RemoveProjectMember(memberId).subscribe({
      next: () => {
        console.log('Member removed successfully');
        this.loadMembers();
      },
      error: (err) => {
        console.error('Failed to remove member', err);
        this.errorMessage = 'Failed to remove member';
      }
    });
  }

  getUserName(userId: string): string {
    const user = this.users.find(user => user.id === userId);
    return user ? user.name : 'No member assigned';
  }

  getUserRole(userId: string): string {
    const user = this.users.find(user => user.id === userId);
    return user ? user.role : 'No member assigned';
  }
}