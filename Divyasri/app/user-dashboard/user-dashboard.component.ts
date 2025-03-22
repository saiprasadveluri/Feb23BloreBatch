import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent {
  taskList: any[] = [];
constructor(private router:Router)
{

}
NavigateToAddNewComment(){
  this.router.navigate(['addcomment'])
}
NavigateToResignTask()
{
  this.router.navigate(['addnewuser'])
}
}
