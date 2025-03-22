import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { TaskInfo } from '../task-info';
 
@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.css']
})
export class CreateTaskComponent {
  frmGroup:FormGroup;
  constructor(private fb:FormBuilder,private srv:DbAccessService,private router:Router)
  {
    this.frmGroup=fb.group({
      projid:new FormControl('',[Validators.required]),
      title:new FormControl('',[Validators.required]),
      tasktype:new FormControl('',[Validators.required]),
      assignedto:new FormControl('',[Validators.required]),
      status:new FormControl('',[Validators.required])
    })
  }
  OnAddClick()
  {
    var projid=this.frmGroup.controls['projid'].value;
    var title=this.frmGroup.controls['title'].value;
    var tasktype=this.frmGroup.controls['tasktype'].value;
    var assignedto=this.frmGroup.controls['assignedto'].value;
    var status=this.frmGroup.controls['status'].value;
    var newObj:TaskInfo={
      projid:projid,
      title:title,
      tasktype:tasktype,
      assignedto:assignedto,
      status:status
    };
    this.srv.AddNewTask(newObj).subscribe({
      next:(res)=>{
        console.log("New task Added"+res.id);
        this.router.navigate(['pmdashboard']);
      },
      error:(err)=>{
 
      }
 
    });
  }
 
}
 
 