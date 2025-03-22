import { Component } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Userinfo } from '../userinfo';
@Component({
  selector: 'app-add-new-user',
  templateUrl: './add-new-user.component.html',
  styleUrls: ['./add-new-user.component.css']
})
export class AddNewUserComponent {
frmGroup: FormGroup;
constructor(private fb: FormBuilder,private srv:DbAccessService,private route:Router) {
this.frmGroup = this.fb.group({
  name:new FormControl('',[Validators.required]),
  uemail:new FormControl('',[Validators.required]),
  upassword:new FormControl('',[Validators.required]),
  role:new FormControl('',[Validators.required]),
});
}
onAddUser(): void {
  var name = this.frmGroup.controls['name'].value;
  var uemail = this.frmGroup.controls['uemail'].value;
  var upassword = this.frmGroup.controls['upassword'].value;
  var role = this.frmGroup.controls['role'].value;
  var obj: Userinfo = {
    name: name,
    email: uemail,
    password: upassword,
    role: role
  };
  this.srv.AddNewUser(obj).subscribe({
    next: (res: Userinfo) => {
      console.log("New User Added: " + res.id);
      this.route.navigate(['/admindashboard']);
    },
    error: (err: any) => {
      console.log(err);
    }
  });
}
}
