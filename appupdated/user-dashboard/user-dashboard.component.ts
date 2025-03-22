import { Component, OnInit } from '@angular/core';
import { Userdata } from '../userdata';
import { MydataService } from '../mydata.service';
import { Adrestaurant } from '../adrestaurant';
import { Aduser } from '../aduser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent implements OnInit{
  loggedInUser:Aduser|undefined;
  nearRestList:Adrestaurant[]=[];
  constructor(private srv:MydataService,private router:Router)
  {

  }
  ngOnInit(): void {
   var strUserData:string|null= sessionStorage.getItem("LoggedInUser")
    if(strUserData!=null)
    {
      this.loggedInUser=JSON.parse(strUserData);
      this.PopulateGrid();
    }
  }
PopulateGrid()
{
  this.srv.getRestaurantList().subscribe({
    next:(res)=>{
      var AllRestList:Adrestaurant[]=res;
        this.nearRestList=AllRestList.filter(r=>r.location.toUpperCase()==this.loggedInUser?.location.toUpperCase())
    }
  })
}
OrderFood(rid:any)
{
  this.router.navigate(['userdashboard/orderfood',rid]);
}
}
