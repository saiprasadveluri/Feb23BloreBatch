import { Component, OnInit } from '@angular/core';
import { Aduser } from '../aduser';

@Component({
  selector: 'app-ad-user-list',
  templateUrl: './ad-user-list.component.html',
  styleUrls: ['./ad-user-list.component.css']
})
export class AdUserListComponent implements OnInit {
  userList:Aduser[]=[];
  ngOnInit(): void {
    this.userList.push({name:'Sai',role:'Admin',location:'HYD'});
    this.userList.push({name:'Durga',role:'Owner',location:'CHN'});
    this.userList.push({name:'Ravi',role:'Owner',location:'HYD'});
    this.userList.push({name:'Shyam',role:'User',location:'HYD'});

  }
  

}
