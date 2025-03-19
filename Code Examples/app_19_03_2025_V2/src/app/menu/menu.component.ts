import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  @Input()LogedInFlag:boolean=false;
  @Output()LoginAction:EventEmitter<boolean>=new EventEmitter<boolean>();
  ngOnInit(): void {
   
  }
  
  LogoutUser()
  {
    this.LoginAction.emit(true);
    this.LogedInFlag=true;
  }

  LoginUser()
  {
    this.LoginAction.emit(false);
    this.LogedInFlag=false;
  }
}
