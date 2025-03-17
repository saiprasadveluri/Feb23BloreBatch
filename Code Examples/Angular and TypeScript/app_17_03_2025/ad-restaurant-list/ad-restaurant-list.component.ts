import { Component, OnInit } from '@angular/core';
import { Adrestaurant } from '../adrestaurant';

@Component({
  selector: 'app-ad-restaurant-list',
  templateUrl: './ad-restaurant-list.component.html',
  styleUrls: ['./ad-restaurant-list.component.css']
})
export class AdRestaurantListComponent implements OnInit{
restList:Adrestaurant[]=[];
ngOnInit(): void {
  this.restList.push({name:'A2B',location:'CHN',owner: 'Durga'});
  this.restList.push({name:'Udipi',location:'BLR',owner: 'Shyam'});
  this.restList.push({name:'Mayuri',location:'HYD',owner: 'Shyam'});
 
}


}
