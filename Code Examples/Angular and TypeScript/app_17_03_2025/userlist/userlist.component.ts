import { Component } from '@angular/core';
import { Userdata } from '../userdata';

@Component({
  selector: 'app-userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css']
})
export class UserlistComponent {
   namesArray:Userdata[]=[
    {userName:"Ravi",age:32,status:1},
    {userName:"Shyam",age:12,status:0},
    {userName:"Mohan",age:22,status:0},
    {userName:"Mary",age:42,status:1},
   ];

   AddUserToArray(evt:any):void{
    console.log(evt)
    let temp:Userdata=evt;
    this.namesArray.push(temp);
   }
}
