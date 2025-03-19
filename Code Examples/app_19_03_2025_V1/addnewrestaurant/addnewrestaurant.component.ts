import { Component } from '@angular/core';
import { MydataService } from '../mydata.service';
import { Adrestaurant } from '../adrestaurant';
import { Router } from '@angular/router';

@Component({
  selector: 'app-addnewrestaurant',
  templateUrl: './addnewrestaurant.component.html',
  styleUrls: ['./addnewrestaurant.component.css']
})
export class AddnewrestaurantComponent {

constructor(private srv:MydataService,private router:Router)
{

}
  OnNewRestaurant(frm:any)
  {
    
    var rname=frm.value.rname;
    var rlocation= frm.value.rlocation;
    var rowner=frm.value.rowner;
    console.log(rname);
    var obj:Adrestaurant={name:rname,location:rlocation,owner:rowner};
    this.srv.AddNewRestaurant(obj).subscribe(
      {
        next:(res)=>{
            this.router.navigate(['admindashboard']);
        },
        error:(err)=>{}
      }
    )

  }
}
