import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Task } from '../task';

@Component({
  selector: 'app-reassigntask',
  templateUrl: './reassigntask.component.html',
  styleUrls: ['./reassigntask.component.css']
})
export class ReassigntaskComponent {
frmGroup:FormGroup;
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
      console.log("reassigned task: " + res.id);
      this.router.navigate(['pm-dashboard']);
    },
    error: (err) => {
      console.error("Error adding task:", err);
    }
  });
}
}
