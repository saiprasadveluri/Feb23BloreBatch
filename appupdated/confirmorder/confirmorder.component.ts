import { Component, OnInit } from '@angular/core';
import { orderdata } from '../OrderData';

@Component({
  selector: 'app-confirmorder',
  templateUrl: './confirmorder.component.html',
  styleUrls: ['./confirmorder.component.css']
})
export class ConfirmorderComponent implements OnInit{
  orders:orderdata[]=[]
  ngOnInit(): void {
  var Orderdata:string|null= sessionStorage.getItem('orderdata');
    if(Orderdata!=null)
    {
     this.orders=JSON.parse(Orderdata);
    }
}



}
