import { Component, OnInit } from '@angular/core';
import { MydataService } from '../mydata.service';
import { Adrestaurant } from '../adrestaurant';
import { Router } from '@angular/router';

@Component({
  selector: 'app-owner-restaurant-list',
  templateUrl: './owner-restaurant-list.component.html',
  styleUrls: ['./owner-restaurant-list.component.css']
})
export class OwnerRestaurantListComponent implements OnInit{
userId:string='';//To Do:
ownRestList:Adrestaurant[]=[]; 
constructor(private srv:MydataService,private router:Router)
{

}
ngOnInit(): void {
  const loggedInUser = JSON.parse(sessionStorage.getItem('LoggedInUser') || '{}');
  this.userId = loggedInUser.id || '';
  this.RefrehView();
}

RefrehView():void{
this.srv.getRestaurantList().subscribe({
  next:(res)=>{
    var allRestList:Adrestaurant[]=<Adrestaurant[]>res;
    this.ownRestList=allRestList.filter(r=>(r.owner==this.userId))
  }
});
}

ManageMenu(rid:string|undefined)
{
this.router.navigate(['managemenu',rid]);
}
}
