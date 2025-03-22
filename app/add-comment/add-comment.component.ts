import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { CommentInfo } from '../comment-info';

@Component({
  selector: 'app-add-comment',
  templateUrl: './add-comment.component.html',
  styleUrls: ['./add-comment.component.css']
})
export class AddCommentComponent {
  frmGroup:FormGroup;
  constructor(private fb:FormBuilder,private srv:DbAccessService,private router:Router)
  {
    this.frmGroup=fb.group({
      taskid:new FormControl('',[Validators.required]),
      commentedby:new FormControl('',[Validators.required]),
      description:new FormControl('',[Validators.required]),
      
    })
  }
  goToQaDashboard(): void {
    this.router.navigate(['qadashboard']); 
  }
  OnAddClick()
  {
    var taskid=this.frmGroup.controls['taskid'].value;
    var commentedby=this.frmGroup.controls['commentedby'].value;
    var description=this.frmGroup.controls['description'].value;
    
    var newObj:CommentInfo={
      taskid:taskid,
      commentedby:commentedby,
      description:description,
      
    };
    this.srv.AddNewComment(newObj).subscribe({
      next:(res)=>{
        console.log("New comment Added"+res.id);
        this.router.navigate(['qadashboard']);
      },
      error:(err)=>{

      }
    });
  }
}

