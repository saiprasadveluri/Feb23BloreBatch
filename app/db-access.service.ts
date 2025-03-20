import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserInfo } from './user-info';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {

  constructor(private http:HttpClient)
   { 

   }

   GetAllUsers():Observable<any>
   {
    return this.http.get(" http://localhost:3004/UserInfo");

   }
   AddNewUser(inpUsr:UserInfo):Observable<any>
   {
    return this.http.post(" http://localhost:3004/UserInfo",inpUsr);

   }
}
