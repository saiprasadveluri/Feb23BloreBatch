import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-memberdashboard',
  standalone: false,
  templateUrl: './memberdashboard.component.html',
  styleUrl: './memberdashboard.component.css'
})
export class MemberdashboardComponent {
  constructor(private route:Router){

  }
  user:string='';
  ngOnInit(){
    var curUser=<UserInfo>JSON.parse(sessionStorage.getItem('LoggedInUserData')||'{}');
    this.user=curUser.role;
  }



}
