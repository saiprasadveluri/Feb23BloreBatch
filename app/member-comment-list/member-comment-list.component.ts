import { Component , OnInit} from '@angular/core';
import { Comments } from '../comments';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-member-comment-list',
  standalone: false,
  templateUrl: './member-comment-list.component.html',
  styleUrl: './member-comment-list.component.css'
})
export class MemberCommentListComponent {
  commentlist:Comments[]=[];
  
  constructor(private srv:DbAccessService){
    
  }

  ngOnInit(): void {
    this.RefreshView();
  }

  RefreshView(){
    var curUser=<UserInfo>JSON.parse(sessionStorage.getItem('LoggedInUserData')||'{}');
    this.srv.GetAllComments().subscribe({
      next:(res)=>{
        var allcommentlist=<Comments[]>res;
        this.commentlist=allcommentlist.filter((comment)=>comment.commentby==curUser.id);
      }
    });
  }
}
