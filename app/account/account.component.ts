import { Component } from '@angular/core';
import { FormBuilder,FormControl,FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';
import { Router } from '@angular/router';


@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent {
//form to get input from user 
frmGroup:FormGroup;
constructor (private fb:FormBuilder,private srv:DbAccessService,private router:Router)
{
  this.frmGroup=fb.group({
    uemail: new FormControl('',[Validators.required]),
    upassword:new FormControl('',[Validators.required]),
   

  });
}
OnLoginClick() {
  const uemail = this.frmGroup.controls['uemail'].value;
  const upassword = this.frmGroup.controls['upassword'].value;

  this.srv.GetAllUsers().subscribe({
    next: (res) => {
      const AllUsers: UserInfo[] = res;
      console.log('All Users:', AllUsers);
      const FilterObj = AllUsers.find((u) => u.email === uemail && u.password === upassword);

      if (FilterObj === undefined) {
        console.error('Invalid login credentials');
      } else {
        console.log('Logged-in User:', FilterObj); // Debugging output
        sessionStorage.setItem('LoggedinUserData', JSON.stringify(FilterObj));

        switch (FilterObj.role.toUpperCase()) {
          case 'ADMIN':
            this.router.navigate(['admindashboard']);
            break;
          case 'PM':
            this.router.navigate(['pmdashboard']);
            break;
          case 'MEMBER':
            this.router.navigate(['memberdashboard']);
            break;
          default:
            console.error('Invalid role:', FilterObj.role);
        }
      }
    },
    error: (err) => {
      console.error('Error fetching users:', err);
    },
  });
}
}


