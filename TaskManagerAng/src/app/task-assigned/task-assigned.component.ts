import { Component } from '@angular/core';
import { UserInfo } from '../user-info';
import { FormGroup,FormControl,FormBuilder,Validators } from '@angular/forms';
import { Projectinfo } from '../projectinfo';
import { Projectmember } from '../projectmember';
import { Tasks } from '../tasks';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-task-assigned',
  templateUrl: './task-assigned.component.html',
  styleUrls: ['./task-assigned.component.css']
})
export class TaskAssignedComponent {
userList:UserInfo[]=[];
projectList:Projectinfo[]=[];
projectmember:Projectmember[]=[];
filteredUsers:UserInfo[]=[];
frmGroup:FormGroup;
constructor(private fb:FormBuilder,private srv:DbAccessService,private router:Router){
  this.frmGroup=fb.group({
    projectid:new FormControl('',Validators.required),
    title:new FormControl('',Validators.required),
    description:new FormControl('',Validators.required),
    tasktype:new FormControl('',Validators.required),
    assignedto:new FormControl('',Validators.required),
    status:new FormControl('',Validators.required)

  });
}
Getprojusers(): void {
  const selectedProjectId = this.frmGroup.controls['projectid'].value;
  console.log('Selected Project ID:', selectedProjectId);
  console.log('Project Members:', this.projectmember);

  const assignedMemberIds = this.projectmember
    .filter((pm) => pm.projectid === selectedProjectId)
    .map((pm) => pm.memberid);

  console.log('Assigned Member IDs:', assignedMemberIds);

  this.filteredUsers = this.userList.filter((user) => user.id && assignedMemberIds.includes(user.id));
  console.log('Filtered users for project:', this.filteredUsers);
}
ngOnInit(): void {
  // Fetch all users
  this.srv.GetAllUsers().subscribe({
    next: (res) => {
      this.userList = res as UserInfo[];
      console.log('Users fetched successfully:', this.userList);
    },
    error: (err) => {
      console.error('Error fetching users:', err);
    }
  });

  // Fetch all projects
  this.srv.GetAllProjects().subscribe({
    next: (res) => {
      this.projectList = res as Projectinfo[];
      console.log('Projects fetched successfully:', this.projectList);
    },
    error: (err) => {
      console.error('Error fetching projects:', err);
    }
  });

  // Fetch all project members
  this.srv.GetAllProjectMember().subscribe({
    next: (res) => {
      this.projectmember = res as Projectmember[];
      console.log('Project members fetched successfully:', this.projectmember);
    },
    error: (err) => {
      console.error('Error fetching project members:', err);
    }
  });
}
OnAddClick(){
  var projectid=this.frmGroup.controls['projectid'].value;
  var title=this.frmGroup.controls['title'].value;
  var description=this.frmGroup.controls['description'].value;
  var tasktype=this.frmGroup.controls['tasktype'].value;
  var assignedto=this.frmGroup.controls['assignedto'].value;
  var status=this.frmGroup.controls['status'].value;
  var newObj:Tasks={
    projectid:projectid,
    title:title,
    description:description,
    tasktype:tasktype,
    assignedto:assignedto,
    status:status
  };
  this.srv.AddNewTask(newObj).subscribe({
    next:(res)=>{
      console.log("New Task Added : " +res.id);
      this.router.navigate(['pmdashboard']);
    },
    error:(err)=>{
      console.log(err);

    }
  });
}
}
