import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DBAccessService } from '../dbaccess.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {
constructor(private router:Router,private srv:DBAccessService){

}
NavigateToAddNewUser(){
  this.router.navigate(['addnewuser'])
}
}
