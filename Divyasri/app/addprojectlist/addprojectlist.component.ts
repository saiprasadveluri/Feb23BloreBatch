import { Component } from '@angular/core';
import { Newproject } from '../newproject';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-addprojectlist',
  templateUrl: './addprojectlist.component.html',
  styleUrls: ['./addprojectlist.component.css']
})
export class AddprojectlistComponent {
  id:string='';
  projectList:Newproject []=[];
  constructor(private srv:DbAccessService,private router:Router)
  {

  }

ngOnInit(): void {      
  this.RefreshView();
}

RefreshView():void
{
  this.srv.getprojectList().subscribe({
          next:(res)=>{
           var AllProjList: Newproject[]=<Newproject[]>res;
           this.projectList=AllProjList;
           console.log("update project list:",this.projectList);
          },
          error:(err)=>{
            console.log(err);
          }
    });
} 


}

