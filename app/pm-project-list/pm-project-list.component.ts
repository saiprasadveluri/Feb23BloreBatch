import { Component, OnInit } from '@angular/core';
import { Project } from '../project';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';
import { ProjectMember } from '../project-member';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pm-project-list',
  templateUrl: './pm-project-list.component.html',
  styleUrls: ['./pm-project-list.component.css']
})
export class PmProjectListComponent implements OnInit {
  projList: Project[] = [];
  users:UserInfo[]=[];
  projmem:ProjectMember[]=[];
  iterate:boolean=true;

  constructor(private srv: DbAccessService,private router:Router) { }

  ngOnInit(): void {
    this.RefreshView();
    
  }
  iterate1():void{
    this.iterate=false;
  }
  iterate2():void{
    this.iterate=true;
  }

  RefreshView(): void {
    var LoggedInUser:UserInfo=JSON.parse(sessionStorage.getItem('LoggedinUserData')as string);
    this.srv.GetAllProjects().subscribe({
      next: (res) => {
        var projList1:Project[] = <Project[]>res;
        this.projList = projList1.filter((u) => u.pm == LoggedInUser.id);
        console.log(this.projList) // Filter projects by user ID
      },
      error: (err) => {
        console.log(err);
      }
    });
    this.getDevQa();
    this.getProjMem();
  }
  getDevQa():void{
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
  }
  getProjMem():void{
    this.srv.GetAllProjMem().subscribe({
      next: (res) =>{
        this.projmem = <ProjectMember[]>res;
      },
      error: (err) => {
        console.log(err);
      }
    })
  }
  AssignUser(id:string,pm:string):void{
    var projid=id;
    var projmanid=pm;
    this.router.navigate(['addprojmem',{ id: projid, pmid: projmanid }])
    
  }
  deletemember(id:string):void{
    this.srv.DeleteProjectMem(id).subscribe({
      next:(res)=>{
        this.ngOnInit();
      },
      error:(err)=>{

      }
    })
  }
}