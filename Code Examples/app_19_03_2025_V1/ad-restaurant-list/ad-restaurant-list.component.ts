import { Component, OnInit } from '@angular/core';
import { Adrestaurant } from '../adrestaurant';
//import { HttpClient } from '@angular/common/http';
import { MydataService } from '../mydata.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-ad-restaurant-list',
  templateUrl: './ad-restaurant-list.component.html',
  styleUrls: ['./ad-restaurant-list.component.css']
})
export class AdRestaurantListComponent implements OnInit{
restList:Adrestaurant[]=[];
constructor(private srv:MydataService,private router:Router)
{
  
}
ngOnInit(): void {
 
  this.srv.getRestaurantList().subscribe(
    {
      next:(res)=>{
        this.restList=res;
      }
    }
  )
 // this.fetchRestData();
}
deleteRest(id:any)
{

}

ShowNewRestaurantScreen()
{
  this.router.navigate(['addnewrestaurant']);
}

/*fetchRestData()
{
  this.http.get("http://localhost:3004/restaurantInfo").subscribe(
    {
      next:(res)=>{
        console.log(res);      
        this.restList=<Adrestaurant[]>res;
      }
    }
  )
}*/
/*deleteRest(id:any)
{
  console.log('About to dlete...')
  this.http.delete(`http://localhost:3004/restaurantInfo/${id}`).subscribe
  ({
    next:(res)=>{
      console.log("Deleted");
      this.fetchRestData();
    }
  })
}*/
}
