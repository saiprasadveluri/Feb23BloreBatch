import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service'
import { ActivatedRoute } from '@angular/router';
import { UserInfo } from '../user-info';
import { ReactiveFormsModule } from '@angular/forms';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-assignmembers',
  standalone: false,
  templateUrl: './assignmembers.component.html',
  styleUrl: './assignmembers.component.css'
})
export class AssignmembersComponent implements OnInit{
  projectId: string | null = null;
  existingMembers: any[] = [];
  selectedUserId: string | null = null;
  existingMemberNames: string[] = [];
  allUsers: UserInfo[] = [];
  assignMemberForm: FormGroup;


  constructor(private route:ActivatedRoute, private srv:DbAccessService, private fb:FormBuilder) { 
    this.assignMemberForm = this.fb.group({
      selectedUserId: new FormControl(null)
    });
  }

  

  ngOnInit(): void {
    this.projectId = this.route.snapshot.paramMap.get('id');
    if(this.projectId){
      this.loadExistingMembers();
    }
  }
  

  loadExistingMembers(): void {
    this.srv.GetProjectMembers(this.projectId!).subscribe({
      next: (members) => {
        this.existingMembers = members;
        console.log('Existing Members:', this.existingMembers);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  loadAllUsers(): void {
    this.srv.GetAllUsers().subscribe({
      next: (users) => {
        this.allUsers = users;
        this.mapExistingMembersToNames();
        console.log('All Users:', this.allUsers);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  mapExistingMembersToNames(): void {
    if (this.existingMembers.length > 0 && this.allUsers.length > 0) {
      this.existingMemberNames = this.existingMembers.map(member => {
        const user = this.allUsers.find(user => user.id === member.userId);
        return user ? user.name : 'Unknown';
      });
    }
  }

  assignMember(): void {
    const selectedUserId = this.assignMemberForm.get('selectedUserId')?.value;
    if (selectedUserId) {
      // Implement the logic to assign the selected user to the project
      console.log(`Assigning user ${selectedUserId} to project ${this.projectId}`);
    }
  }
}
