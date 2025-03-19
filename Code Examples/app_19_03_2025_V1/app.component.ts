import { AfterViewInit, Component, ElementRef, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { Aduser } from './aduser';
import { Router, RouterOutlet } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { MychangeemitterService } from './mychangeemitter.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  
  isLogedIn:boolean=false;
  constructor(private route:Router,private src:MychangeemitterService )
  {
    this.src.EmittedEvents.subscribe((val:Boolean)=>{
      this.isLogedIn=true;
    });
  }  
  
  DoLoginAction(flag:any)
  {    
    sessionStorage.clear();
    this.isLogedIn=false;
    this.route.navigate(['account']);   
  }
  
}