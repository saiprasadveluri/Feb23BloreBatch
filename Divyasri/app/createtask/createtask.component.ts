import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Task } from '../task';

@Component({
  selector: 'app-createtask',
  templateUrl: './createtask.component.html',
  styleUrls: ['./createtask.component.css']
})
export class CreatetaskComponent {
 frmGroup:FormGroup;
 availableProject: any[] = [];
 projectList: any[] = []; // List of projects
 memberList: any[] = [];
     constructor(private fb:FormBuilder,private srv:DbAccessService,private router:Router)
     {
       this.frmGroup=fb.group({
        projid:new FormControl('',[Validators.required]),
        title:new FormControl('',[Validators.required]),
        tasktype:new FormControl('',[Validators.required]),
        status :new FormControl('',[Validators.required]),
        
    
        assignedto:new FormControl(''),
   
       })
     }
     
  ngOnInit(): void {
    this.loadProjects(); // Load the list of projects
  }

  // Load the list of projects
  loadProjects(): void {
    this.srv.getprojectLists().subscribe({
      next: (res) => {
        this.projectList = res;
        console.log('Projects:', this.projectList);
      },
      error: (err) => {
        console.error('Error fetching projects:', err);
      }
    });
  }

  // Load the list of members for the selected project
  loadMembers(projid: string): void {
    this.srv.getProjectMember(projid).subscribe({
      next: (res) => {
        this.memberList = res;
        console.log('Members:', this.memberList);
      },
      error: (err) => {
        console.error('Error fetching members:', err);
      }
    });
  }
     onAddClick() {
       var  projid = this.frmGroup.controls[' projid'].value;
       var  title = this.frmGroup.controls[' title'].value;
       var tasktype = this.frmGroup.controls['tasktype'].value;
       var  status= this.frmGroup.controls[' status'].value;
       var  assignedto= this.frmGroup.controls[' assignedto'].value;
     
       var newObj: Task = {
        projid:  projid,
        title: title,
        tasktype:tasktype,
        status:status,
        assignedto:assignedto
       };
     
       this.srv.AddNewTask(newObj).subscribe({
         next: (res ) => {
           console.log("New Task Added: " + res.id);
           this.router.navigate(['pm-dashboard']);
         },
         error: (err) => {
           console.error("Error adding task:", err);
         }
       });
     }
 
   
}
