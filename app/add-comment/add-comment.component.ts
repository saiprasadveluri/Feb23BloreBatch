import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserInfo } from '../user-info';
import { Comment } from '../comment';

@Component({
  selector: 'app-add-comment',
  templateUrl: './add-comment.component.html',
  styleUrls: ['./add-comment.component.css']
})
export class AddCommentComponent implements OnInit {
  tid:string='';
  LoggedInUser:UserInfo=JSON.parse(sessionStorage.getItem('LoggedinUserData')as string);
  tit:string='';
  cid:string='';
  
  frmGroup:FormGroup;
  constructor(private srv:DbAccessService,private route:ActivatedRoute,private router:Router,private fb:FormBuilder){
      this.frmGroup=fb.group({
              desc:new FormControl('',[Validators.required])
            })
    }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params=>{
      this.tid = params.get('id')!;
      this.cid = params.get('cid')!;
      this.tit = params.get('title')!;
    })
  }
  OnAddComment():void{
    var desc = this.frmGroup.controls['desc'].value;
    
    var obj:Comment={taskid:this.tid,commentby:this.cid,title:this.tit,description:desc}
    this.srv.AddComment(obj).subscribe(
      {
        next:(res)=>{
          this.router.navigate(['memberdashboard']);
        },
        error:()=>{

        }
      }
    )
  }
}
