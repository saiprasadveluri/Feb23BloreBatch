import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DbAccessService } from '../db-access.service';
import { ProjectMember } from '../project-member';
import { Task } from '../task';

@Component({
  selector: 'app-project-members',
  templateUrl: './project-members.component.html',
  styleUrls: ['./project-members.component.css']
})
export class ProjectMembersComponent implements OnInit {
  projectId: string = ''; 
  members: ProjectMember[] = []; 
  newMemberName: string = ''; 
  newMemberRole: 'Developer' | 'QA' = 'Developer';
   
  constructor(private route: ActivatedRoute, private dbService: DbAccessService) {}

  ngOnInit() {
    this.projectId = this.route.snapshot.paramMap.get('projectId') || ''; 
    this.loadMembers(); 
  }

  loadMembers() {
    this.dbService.getProjectMembers(this.projectId).subscribe(
      (data) => {
        this.members = data;
      },
      (error) => {
        console.error('Error fetching project members:', error);
      }
    );
  }

  assignMember() {
    const newMember: ProjectMember = {
      id: new Date().getTime().toString(), 
      name: this.newMemberName,
      role: this.newMemberRole
    };

    this.dbService.assignMemberToProject(this.projectId, newMember).subscribe(
      () => {
        this.loadMembers(); 
        this.newMemberName = '';
        this.newMemberRole = 'Developer';
      },
      (error) => {
        console.error('Error assigning member:', error);
      }
    );
  }

  removeMember(memberId: string) {
    this.dbService.removeMemberFromProject(this.projectId, memberId).subscribe(
      () => {
        this.loadMembers(); 
      },
      (error) => {
        console.error('Error removing member:', error);
      }
    );
  }

  
}
