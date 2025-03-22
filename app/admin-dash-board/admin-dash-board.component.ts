import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-admin-dash-board',
  standalone: false,
  templateUrl: './admin-dash-board.component.html',
  styleUrl: './admin-dash-board.component.css'
})
export class AdminDashBoardComponent {
  constructor(private router:Router){
    
  }
  NavigateToAddNewUser(){
      this.router.navigate(['addnewuser']);
  }

  NavigateToAddNewProject(){ 
      this.router.navigate(['addnewproject']);
  }
}
