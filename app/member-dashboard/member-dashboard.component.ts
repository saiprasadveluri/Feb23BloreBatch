import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { ProjMember } from '../proj-member';

@Component({
  selector: 'app-member-dashboard',
  templateUrl: './member-dashboard.component.html',
  styleUrls: ['./member-dashboard.component.css']
})
export class MemberDashboardComponent {
  membList:ProjMember[]=[];
    constructor(private srv:DbAccessService,private router:Router)
    {
    
    }
  
    goToPMDashboard(): void {
      this.router.navigate(['pmdashboard']); // Change to your actual PM dashboard route
    }
    ngOnInit(): void {      
      this.RefreshView();
    }
  
    RefreshView():void
    {
      this.srv.GetAllMembers().subscribe({
              next:(res)=>{
                this.membList=<ProjMember[]>res;
              },
              error:(err)=>{
                console.log(err);
              }
        });
    }  

}
