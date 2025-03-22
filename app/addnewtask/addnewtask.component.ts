import { Component } from '@angular/core';
import {UserInfo} from '../user-info';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { ProjectMember } from '../project-member';
import { Project } from '../project'; 
import { Task } from '../task';

@Component({
  selector: 'app-addnewtask',
  standalone: false,
  templateUrl: './addnewtask.component.html',
  styleUrl: './addnewtask.component.css'
})
export class AddnewtaskComponent {
  projectId: string | null = null;
  allUsers: UserInfo[] = [];
  selectedProjectMembers: ProjectMember[] = [];
  existingMembers: any[] = [];
  selectedUserId: string | null = null;
  existingMemberNames: string[] = [];

  frmGroup:FormGroup;
  constructor(private fb:FormBuilder, private srv:DbAccessService, private router :Router ){ this.frmGroup=fb.group({ 
    title:new FormControl('', [Validators.required]),
    tasktype:new FormControl('', [Validators.required]),
    assignedTo:new FormControl('', [Validators.required]),
    status:new FormControl('', [Validators.required]),
  })
}

OnAddClick(){
  var title = this.frmGroup.controls['title'].value;
  var tasktype = this.frmGroup.controls['tasktype'].value;
  var assignedTo = this.frmGroup.controls['assignedTo'].value;
  var status = this.frmGroup.controls['status'].value;

  console.log('Form valid:', this.frmGroup.valid); // Logs true or false
    
  
  
    var newObj:Task={
      title:title,
      tasktype:tasktype,
      assignedTo:assignedTo,
      status:status

    };

    this.srv.AddNewTask(newObj).subscribe({
      next:(res)=>{
      console.log("New Task Added",res.id);
      this.router.navigate(['pmdashboard']);
      },
      error:(err)=>{

      }
    })
}

GetMembersForProject(projectId: string): void {
    if (!projectId) {
      console.error('Project ID is undefined');
      return;
    }
    this.srv.GetProjectMembers(projectId).subscribe({
      next: (members) => {
        this.selectedProjectMembers = <ProjectMember[]>members;
        
        console.log('Members for Project:', this.selectedProjectMembers);
      },
      error: (err) => {
        console.log(err);
      }
    });
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

}
