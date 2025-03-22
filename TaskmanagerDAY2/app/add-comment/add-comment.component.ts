import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Commentinfo } from '../commentinfo';

@Component({
  selector: 'app-add-comment',
  templateUrl: './add-comment.component.html',
  styleUrls: ['./add-comment.component.css']
})
export class AddCommentComponent implements OnInit {
  taskid: string = '';
  commentedby: string = '';
  commentForm: FormGroup;

  constructor(private route: ActivatedRoute,
    private fb: FormBuilder,
    private srv: DbAccessService,
    private router: Router
  ) {
    this.commentForm = this.fb.group({
      description: new FormControl('', [Validators.required])
    });
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.taskid = params['taskid'];
      this.commentedby = params['commentedby'];
      console.log('Task ID:', this.taskid);
      console.log('Commented By:', this.commentedby);
    });
  }

  onSubmit(): void {
    if (this.commentForm.invalid) {
      console.error('Form is invalid');
      return;
    }

    const newComment: Commentinfo = {
      taskid: this.taskid,
      commentedby: this.commentedby,
      description: this.commentForm.controls['description'].value
    };

    this.srv.AddComment(newComment).subscribe({
      next: (res) => {
        console.log('Comment added successfully:', res);
        this.router.navigate(['memberdashboard']); // Navigate back to the member dashboard
      },
      error: (err) => {
        console.error('Error adding comment:', err);
      }
    });
  }
}