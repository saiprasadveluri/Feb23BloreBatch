import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';
import { Router } from '@angular/router';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent {
  frmGroup: FormGroup;
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private srv: DbAccessService, private router: Router) {
    this.frmGroup = fb.group({
      uemail: new FormControl('', [Validators.required]),
      upassword: new FormControl('', [Validators.required]),
    });
  }

  OnLoginClick() {
    const uemail = this.frmGroup.controls['uemail'].value;
    const upassword = this.frmGroup.controls['upassword'].value;

    this.srv.GetAllUsers().subscribe({
      next: (res) => {
        const AllUsers = <UserInfo[]>res;
        console.log(AllUsers);
        const FilterObj = AllUsers.find((u) => u.email === uemail && u.password === upassword);
        if (FilterObj === undefined) {
          this.errorMessage = 'Invalid login credentials';
          console.error('Invalid login credentials');
        } else {
          sessionStorage.setItem('LoggedinUserData', JSON.stringify(FilterObj));
          switch (FilterObj.role.toUpperCase()) {
            case 'ADMIN':
              this.router.navigate(['admindashboard']);
              break;
            case 'PM':
              this.router.navigate(['pmdashboard']);
              break;
            case 'DEVELOPER':
            case 'QA':
              this.router.navigate(['user-dashboard']);
              break;
            default:
              this.errorMessage = 'Invalid role';
              console.error('Invalid role');
              break;
          }
        }
      },
      error: (err) => {
        this.errorMessage = 'An error occurred during login';
        console.error('An error occurred during login', err);
      }
    });
  }
}