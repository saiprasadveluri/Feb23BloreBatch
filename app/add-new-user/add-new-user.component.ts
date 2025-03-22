import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Userinfo } from '../userinfo';
import { TmaccessService } from '../tmaccess.service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-add-new-user',
  templateUrl: './add-new-user.component.html',
  styleUrls: ['./add-new-user.component.css']
})
export class AddNewUserComponent {
    frmGroup:FormGroup;
    constructor(private fb:FormBuilder,private srv:TmaccessService,private router:Router)
    {
      this.frmGroup=fb.group({
        name:new FormControl('',[Validators.required]),
        email:new FormControl('',[Validators.required]),
        password:new FormControl('',[Validators.required]),
        role:new FormControl('',[Validators.required])
      })
    }

    OnAddClick()
    {
      var uname=this.frmGroup.controls['name'].value;
      var uemail=this.frmGroup.controls['email'].value;
      var upassword=this.frmGroup.controls['password'].value;
      var urole=this.frmGroup.controls['role'].value;
      var obj:Userinfo={
        name:uname,
        email:uemail,
        password:upassword,
        role:urole
      };
      this.srv.AddNewUser(obj).subscribe({
        next: (res) => {
          console.log("new user added: " + res.id);
          this.router.navigate(['admindashboard']);
        },
        error: (err) => {}
      });
    }
}
