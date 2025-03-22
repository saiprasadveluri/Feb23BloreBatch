import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { CommentInfo } from '../comment-info';

@Component({
  selector: 'app-view-comment',
  templateUrl: './view-comment.component.html',
  styleUrls: ['./view-comment.component.css']
})
export class ViewCommentComponent {
  commentList:CommentInfo[]=[];
    constructor(private srv:DbAccessService,private router:Router)
    {
    
    }
  
    goToQaDashboard(): void {
      this.router.navigate(['qadashboard']); 
    }
    ngOnInit(): void {      
      this.RefreshView();
    }
  
    RefreshView():void
    {
      this.srv.GetAllComments().subscribe({
              next:(res)=>{
                this.commentList=<CommentInfo[]>res;
              },
              error:(err)=>{
                console.log(err);
              }
        });
    }  

}





