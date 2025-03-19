import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MydataService } from '../mydata.service';
import { Menuitem } from '../menuitem';

@Component({
  selector: 'app-manage-menu',
  templateUrl: './manage-menu.component.html',
  styleUrls: ['./manage-menu.component.css']
})
export class ManageMenuComponent implements OnInit{
  restId:string|null='';
  restMenuitems:Menuitem[]=[];

  constructor(private srv:MydataService, private router:Router,private curRoute:ActivatedRoute)
  {
    this.curRoute.paramMap.subscribe((pm)=>{
                              this.restId=pm.get('rid')
                                          });
  }
  ngOnInit(): void {
    //Fetch All Menu Items
    this.srv.getAllMenuItems().subscribe(
      {
        next:(res)=>{
          var allMenu:Menuitem[]=<Menuitem[]>res;
            if(allMenu.length>0)
            {
              this.restMenuitems=allMenu.filter((m)=>m.rid==this.restId);
            }
        },
        error:(err)=>{}
      }
    )
    //Filter Menu Items based on RestId
    //Display
  }

  OnFormSubmit(frm:any)
  {
    console.log(frm.value.dishType)
  }
}
