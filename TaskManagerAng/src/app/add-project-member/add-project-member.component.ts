import { Component, OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserInfo } from '../user-info';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectMember } from '../project-member';
import { Project } from '../project';

@Component({
  selector: 'app-add-project-member',
  templateUrl: './add-project-member.component.html',
  styleUrls: ['./add-project-member.component.css']
})
export class AddProjectMemberComponent implements OnInit {
  pid:string='';
  pmid:string='';
  users:UserInfo[]=[];
  projmem:ProjectMember[]=[];
  projList: Project[] = [];
  frmGroup:FormGroup;
  constructor(private srv:DbAccessService,private route:ActivatedRoute,private router:Router,private fb:FormBuilder){
    this.frmGroup=fb.group({
            uid:new FormControl('',[Validators.required])
          })
  }
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.pid = params.get('id')!;
      this.pmid= params.get('pmid')!;
    });
    this.srv.GetAllUsers().subscribe({
          next:(res)=>{
            var user:UserInfo[] = <UserInfo[]>res;
            this.users=user.filter((u)=> u.role.toUpperCase()=='DEVELOPER' || u.role.toUpperCase()=='QA')
            console.log(this.users)
          },
          error: (err) => {
            console.log(err);
          }
        })
    this.srv.GetAllProjMem().subscribe({
      next:(res)=>{
        var mem:ProjectMember[] = <ProjectMember[]>res;
        this.projmem=mem.filter((u)=> u.projid==this.pid)
        console.log(this.users)
      },
      error: (err) => {
        console.log(err);
      }
    })
    this.srv.GetAllProjects().subscribe({
      next: (res) => {
        var projList1:Project[] = <Project[]>res;
        this.projList = projList1.filter((u) => u.id==this.pid);
        console.log(this.projList) // Filter projects by user ID
      },
      error: (err) => {
        console.log(err);
      }
    });
    
  }

  OnAddProjectManager():void{
    var uid = this.frmGroup.controls['uid'].value;

    var obj:ProjectMember={projid:this.pid,memid:uid}
    this.srv.AddNewProjectMem(obj).subscribe(
      {
        next:(res)=>{
          this.router.navigate(['pmdashboard']);
        },
        error:()=>{

        }
      }
    )
  }

}
