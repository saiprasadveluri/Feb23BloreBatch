import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
 
 
@Component({
  selector: 'app-pm-dashboard',
  templateUrl: './pm-dashboard.component.html',
  styleUrls: ['./pm-dashboard.component.css']
})
export class PmDashboardComponent {
 
  constructor(private router:Router)
    {
     
    }
   
    NavigateToAddNewProject()
    {
      this.router.navigate(['addnewproject']);
    }
   
 
   
 
}

