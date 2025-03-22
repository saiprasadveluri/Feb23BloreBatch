import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MydataService } from '../mydata.service';
import { Menuitem } from '../menuitem';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { orderdata } from '../OrderData';

@Component({
  selector: 'app-order-food',
  templateUrl: './order-food.component.html',
  styleUrls: ['./order-food.component.css']
})
export class OrderFoodComponent implements OnInit,AfterViewInit{
  restId:string|null=null;
  RestMenuArray:Menuitem[]=[];
  myForm:FormGroup=new FormGroup({});
  placedOrder:orderdata[]=[];
  constructor(private router:Router,private curRoute:ActivatedRoute,private srv:MydataService,private fb:FormBuilder)
  {
    sessionStorage.removeItem('orderdata');
   this.curRoute.paramMap.subscribe((pm)=>{
    this.restId=pm.get('rid');
    console.log(this.restId)
    if(this.restId!=null)
      {
          this.srv.getAllMenuItems().subscribe(
            {
              next:(res)=>{
              var AllMenuItems:Menuitem[]=<Menuitem[]>res;
              
              this.RestMenuArray=AllMenuItems.filter((mi)=>mi.rid==this.restId);
              this.myForm=this.PrepareForm(this.RestMenuArray);              
            }
            }
          )
      }
   })
  }
  

  PrepareForm(inpMenuArr:Menuitem[]):FormGroup
  {  
    var fgrp=this.fb.group(
      {
        frmArr:new FormArray<FormGroup>([])
      }
    );
    for(var itm of inpMenuArr)
    {
      //console.log(itm)
      var subfrmGroup=new FormGroup({
          did:new FormControl(itm.id),
          dname:new FormControl(itm.dishname),
          dprice:new FormControl(itm.price),
          qty:new FormControl('0'),
      });
      //console.log(fgrp)
      var frmArrTemp=fgrp.controls['frmArr'];
      
      frmArrTemp.push(subfrmGroup);
      //console.log(frmArrTemp)
    }
    return fgrp;
  }
  get Sections():FormArray
  {
    return this.myForm?.controls['frmArr'] as FormArray;
  }

  CurGroup(indx:number):FormGroup<any>
  {
    if(this.myForm!=undefined)
    {
      var frmArr=<FormArray>this.myForm.controls['frmArr'];
      
      var fgrp:FormGroup<any>=<FormGroup>frmArr.controls[indx];
      //console.log(fgrp)
      
          return fgrp;
       
    }
    else
    {
      return new FormGroup({});
    }
    
  }
  ngOnInit(): void {    
    
  }

  ngAfterViewInit(): void {
    
  }

  PlaceOrder()
  {
    console.log(this.myForm.controls['frmArr'])
    var frmArrObj=<FormArray>this.myForm.controls['frmArr'];
    for(var fobj of frmArrObj.controls)
    {
        var fg=<FormGroup>fobj;
        var itemId=fg.controls['did'].value;
        var dname=fg.controls['dname'].value;
        var Qty=parseInt(fg.controls['qty'].value);
        var obj={id:itemId,name:dname,qty:Qty};
        console.log(obj)
        if(Qty>0)
        this.placedOrder.push(obj);        
    }
    if(this.placedOrder.length>0)
    {
    sessionStorage.setItem("orderdata",JSON.stringify(this.placedOrder));
    this.router.navigate(['userdashboard/confirmorder']);
    }
  }

}
