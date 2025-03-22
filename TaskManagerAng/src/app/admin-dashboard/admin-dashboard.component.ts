import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {
  constructor(private router:Router){}
  NavToAddNewUser(){
    this.router.navigate(['addnewuser'])
  } 
  NavToAddNewProject(){
    this.router.navigate(['addnewproject'])
  }
}
