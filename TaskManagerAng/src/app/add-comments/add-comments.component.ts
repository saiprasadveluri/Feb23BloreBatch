import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Comments } from '../comments';

@Component({
  selector: 'app-add-comments',
  templateUrl: './add-comments.component.html',
  styleUrls: ['./add-comments.component.css']
})
export class AddCommentsComponent {
taskId:string='';
commentedby:string='';
commentForm:FormGroup;

constructor(private fb:FormBuilder,private srv:DbAccessService,private route:ActivatedRoute,private router:Router){
  this.commentForm=this.fb.group({
    description: new FormControl('',[Validators.required])
  });
}
ngOnInit():void{
  this.route.queryParams.subscribe(params=>{
    this.taskId=params['taskId'];
    this.commentedby=params['commentedby'];
    console.log('Task ID:',this.taskId);
    console.log('Commented By:',this.commentedby);
  });
}

OnAddClick(){
  if(this.commentForm.invalid){
    console.error('Form is invalid');
    return;
  }

  const newComment :Comments={
    taskid:this.taskId,
    commentedby:this.commentedby,
    description:this.commentForm.controls['description'].value
  };
  this.srv.AddComment(newComment).subscribe({
    next:(res)=>{
      console.log('Comment Added Successfully',res);
      this.router.navigate(['memberdashboard']);
    },error:(err)=>{
      console.error('Error in adding comment',err);
    }
  });

}
}
