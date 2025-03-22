import { Component, OnInit } from '@angular/core';
import { ProjectMember } from '../project-member';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Task } from '../task';
import { Project } from '../project';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-member-update-task',
  templateUrl: './member-update-task.component.html',
  styleUrls: ['./member-update-task.component.css']
})
export class MemberUpdateTaskComponent implements OnInit {
  projMem: ProjectMember[] = [];
  frmGroup: FormGroup;
 
  users:UserInfo[]=[];
  id:string='';
  pid:string='';
  LoggedInUser:UserInfo=JSON.parse(sessionStorage.getItem('LoggedinUserData')as string);
  constructor(private fb: FormBuilder, private srv: DbAccessService, private router: Router, private route: ActivatedRoute) {
    this.frmGroup = fb.group({
      
        
        mid:new FormControl('',[Validators.required]),
        
      
    });
  }
  
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.id=params.get('id')!;
      this.pid=params.get('pid')!;
    })
      this.srv.GetAllUsers().subscribe({
        next:(res)=>{
          var user:UserInfo[]=<UserInfo[]>res;
          if(this.LoggedInUser.role=='QA'){
          this.users=user.filter((u)=>u.role=='DEVELOPER')
          }else if (this.LoggedInUser.role=='DEVELOPER'){
            this.users=user.filter((u)=>u.role=='QA')
          }
          console.log(this.users)
        },
        error:(err)=>{

        }
      })
    this.srv.GetAllProjMem().subscribe({
      next:(res)=>{
        var pm:ProjectMember[]=<ProjectMember[]>res;
        this.projMem = pm.filter((u) => u.projid == this.pid && this.users.some(user => u.memid == user.id));
        console.log(this.projMem);
      },
      error:(err)=>{

      }
    })
  }
  UpdateTask():void{
      var mid=this.frmGroup.controls['mid'].value;
      console.log(this.id)
      console.log(mid);
      this.srv.UpdateTaskAssignedTo(this.id,mid).subscribe({
        next:(res)=>{
          this.router.navigate(['memberdashboard'])
        },
        error:()=>{

        }
      })
  }
}
