import { Component,OnInit } from '@angular/core';
import { DbAccessService } from '../db-access.service';
import { Projectinfo } from '../projectinfo';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent implements OnInit {
  projList:Projectinfo[]=[];

  constructor(private srv:DbAccessService){

  }

  ngOnInit():void{
    // var srv = new MydataService();
   this.RefreshView();
  }

  RefreshView():void{
    this.srv.GetAllProjects().subscribe({
      next:(res)=>{
      this.projList=<Projectinfo[]>res;
    },
    error:(err)=>{
      console.log(err);
    }
  });
  }

  DeleteUser(id:string):void{
    this.srv.DeleteProject(id).subscribe({
      next:()=>{
        this.RefreshView();
      },
      error:(err)=>{

      }
    })
  }
}
