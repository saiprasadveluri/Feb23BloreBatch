import { Component, EventEmitter, Output } from '@angular/core';
import { Userdata } from '../userdata';

@Component({
  selector: 'app-adduser',
  templateUrl: './adduser.component.html',
  styleUrls: ['./adduser.component.css']
})
export class AdduserComponent {
   uname:string='';
   uage:number=0;
   charsRemaing:number=40;
   @Output() AddUserEvent:EventEmitter<Userdata>=
   new EventEmitter<Userdata>();
   AddNewUser()
   {
    let usrdata:Userdata={
      userName:this.uname,
      age:this.uage,
      status:1
    };

    this.AddUserEvent.emit(usrdata);

    console.log(this.uname);
    console.log(this.uage);
   }

   OnTextChange()
   {
     this.charsRemaing=40-this.uname.length;
     if(this.uname.length>=40)
     {
      this.uname=this.uname.substring(0,39);
     }
   }
}
