import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfo } from '../user-info';
@Component({
  selector: 'app-pm-dashboard',
  templateUrl: './pm-dashboard.component.html',
  styleUrls: ['./pm-dashboard.component.css']
})
export class PmDashboardComponent {
  constructor(private router:Router){}
  NavToAddNewTask():void{
    

  }
}
