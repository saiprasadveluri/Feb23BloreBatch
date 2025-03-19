import { Injectable } from '@angular/core';
import { Aduser } from './aduser';
import { Adrestaurant } from './adrestaurant';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MydataService {
  constructor(private http:HttpClient) 
  { 

  }
  
  getUserList():Observable<any>
  {
    return this.http.get("http://localhost:3004/userInfo");
    /*return [
      {id:"1",name: "Sai",role:"Admin",location:"HYD",email:"admin@admin.com",password:"admin"},
      {id:"2",name: "Durga",role:"Owner",location:"HYD",email:"durga@durga.com",password:"123456"}
    ];*/

  }
  deleteUser(id:string):Observable<any>
  {
    return this.http.delete(`http://localhost:3004/userInfo/${id}`);
  }

  getRestaurantList():Observable<any>
  {
    return this.http.get("http://localhost:3004/restaurantInfo");
  }

  getAllMenuItems()
  {
    return this.http.get("http://localhost:3004/menu");
  }

  AddNewRestaurant(obj:Adrestaurant)
  {
    return this.http.post("http://localhost:3004/restaurantInfo",obj);
  }
}
