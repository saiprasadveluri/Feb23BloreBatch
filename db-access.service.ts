import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserInfo } from './user.info';

@Injectable({
  providedIn: 'root'
})
export class DbAccessService {

  constructor(private http:HttpClient) { }

  GetAllUsers():Observable<any>{
    return this.http.get("http://localhost:5004/UserInfo")
  }
  AddNewUser(inpUsr:UserInfo):Observable<UserInfo>
  {
    return  this.http.post<UserInfo>("http://localhost:5004/UserInfo",inpUsr);
  }
}
