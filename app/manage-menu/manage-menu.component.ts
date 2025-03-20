import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MydataService } from '../mydata.service';
import { Menuitem } from '../menuitem';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-manage-menu',
  templateUrl: './manage-menu.component.html',
  styleUrls: ['./manage-menu.component.css']
})
export class ManageMenuComponent implements OnInit{
  restId:string|null='';
  restMenuitems:Menuitem[]=[];
  frmMenuGroup:FormGroup;
  constructor(private srv:MydataService, private router:Router,private curRoute:ActivatedRoute,private fb:FormBuilder)
  {
    this.frmMenuGroup=fb.group({
        dishName:new FormControl('',[Validators.required]),
        dishType:new FormControl('',[Validators.required]),
        dishPrice:new FormControl('',[Validators.required])
    });
    this.curRoute.paramMap.subscribe((pm)=>{
                              this.restId=pm.get('rid')
                                          });
  }
  ngOnInit(): void {
    //Fetch All Menu Items
    this.RefreshGrid();
    
  }


   RefreshGrid()
   {
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
   }
  OnNewDishAdd()
  {
    console.log(this.frmMenuGroup.value);
    var menuitem:Menuitem={
      dishname:this.frmMenuGroup.value.dishName,
      dishtype:this.frmMenuGroup.value.dishType,
      price:this.frmMenuGroup.value.dishPrice,
      rid:this.restId!=null?this.restId:'0'
    }
    this.srv.AddNewMenuItem(menuitem).subscribe({
      next:(res)=>{
          this.RefreshGrid();
          this.frmMenuGroup.reset();
      }
    })
  }

  IsControlNotValid(ctrlName:string):boolean
  {
    return (this.frmMenuGroup.controls[ctrlName].touched && !this.frmMenuGroup.controls[ctrlName].valid);
  }

  IsFormValid():boolean{
    return this.frmMenuGroup.valid;
  }
}
