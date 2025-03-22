import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-admindashboard',
  standalone: false,
  templateUrl: './admindashboard.component.html',
  styleUrls: ['./admindashboard.component.css']
})
export class AdmindashboardComponent {
 constructor(private router:Router){
    
  }
  NavigateToAddNewUser(){
      console.log('pewww');
      this.router.navigate(['adduser']);

  }
}
