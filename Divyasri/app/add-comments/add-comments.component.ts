import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Comment as AppComment } from '../comment'; // Alias to avoid conflict with global Comment type
@Component({
  selector: 'app-add-comments',
  templateUrl: './add-comments.component.html',
  styleUrls: ['./add-comments.component.css']
})
export class AddCommentsComponent {
frmGroup:FormGroup;
constructor(private fb:FormBuilder,private srv:DbAccessService,private router:Router)
{
  this.frmGroup=fb.group({
    taskid :new FormControl(''),
    Commentedby:new FormControl(''),
    description:new FormControl('',[Validators.required]),
  
  })
}
onAddClick() {
  var taskid = this.frmGroup.controls['taskid'].value;
  var  Commentedby = this.frmGroup.controls['email'].value;
  var description = this.frmGroup.controls[' description'].value;



  const cmt: AppComment = {
    taskid: taskid,
    Commentedby: Commentedby,
    description: description
  }; // Use AppComment consistently to avoid conflicts

  this.srv.AddNewComment(cmt).subscribe({
    next: (res) => {
      console.log("New Comment Added: " + res.id);
      this.router.navigate(['user-dashboard']);
    },
    error: (err) => {
      console.error("Error adding Comment:", err);
    }
  }); // Properly close the subscribe block
}
}
