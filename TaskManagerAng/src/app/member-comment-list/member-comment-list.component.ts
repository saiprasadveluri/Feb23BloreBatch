import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { Comment } from '../comment';
import { UserInfo } from '../user-info';
import { Router } from '@angular/router';

@Component({
  selector: 'app-member-comment-list',
  templateUrl: './member-comment-list.component.html',
  styleUrls: ['./member-comment-list.component.css']
})
export class MemberCommentListComponent implements OnInit {
  comList:Comment[]=[];
  LoggedInUser:UserInfo=JSON.parse(sessionStorage.getItem('LoggedinUserData')as string);
  constructor(private srv:DbAccessService,private router:Router){}
  ngOnInit(): void {
    this.srv.GetAllComment().subscribe({
      next:(res)=>{
        var comList1:Comment[]=<Comment[]>res;
        this.comList=comList1.filter((u)=>u.commentby==this.LoggedInUser.id)
        console.log(this.comList);
      }
    })
  }
}
