// import { Component } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { DbAccessService } from '../db-access.service';
// import { Router } from '@angular/router';
// import { ProjInfo } from '../proj-info';

// @Component({
//   selector: 'app-view-own-projects',
//   templateUrl: './view-own-projects.component.html',
//   styleUrls: ['./view-own-projects.component.css']
// })
// export class ViewOwnProjectsComponent {
//   userId:string="07d3";//To Do:
//   ownprojList:ProjInfo[]=[]; 
//   constructor(private srv:DbAccessService,private router:Router)
//   {
  
//   }
//   ngOnInit(): void {
//     this.RefrehView();
//   }
  
//   RefrehView():void{
//   this.srv.GetAllProjects().subscribe({
//     next:(res)=>{
//       var allProjList:ProjInfo[]=<ProjInfo[]>res;
//       this.ownprojList=allProjList.filter(r=>r.pm==this.userId)
//     }
//   });
//   }
  
//   // ManageMenu(rid:string|undefined)
//   // {
//   // this.router.navigate(['managemenu',rid]);
//   // }  implements OnInit
// }

import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { ProjInfo } from '../proj-info';

@Component({
  selector: 'app-view-own-projects',
  templateUrl: './view-own-projects.component.html',
  styleUrls: ['./view-own-projects.component.css']
})
export class ViewOwnProjectsComponent {
  userId:string="07d3";//To Do:
  ownprojList:ProjInfo[]=[]; 
  constructor(private srv:DbAccessService,private router:Router)
  {
  
  }

  goToPMDashboard(): void {
    this.router.navigate(['pmdashboard']); // Change to your actual PM dashboard route
  }
  ngOnInit(): void {
    this.RefrehView();
    
  }
  
  RefrehView():void{
  this.srv.GetAllProjects().subscribe({
    next:(res)=>{
      var allProjList:ProjInfo[]=<ProjInfo[]>res;
      this.ownprojList=allProjList.filter(r=>r.pm==this.userId)
      
    }
    
    
  });

  
  }
  
  // ManageMenu(rid:string|undefined)
  // {
  // this.router.navigate(['managemenu',rid]);
  // }  implements OnInit
}









