import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormControl,Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { DbAccessServiceService } from '../db-access-service.service';
import { UserInfo } from '../userinfo';
import { Observable } from 'rxjs';
import { Router } from '@angular/router'; 
@Component({
  selector: 'app-accounts',
  standalone: false,
  templateUrl: './accounts.component.html',
  styleUrl: './accounts.component.css'
})
export class AccountsComponent {
  frmGroup: FormGroup;

  constructor(private fb: FormBuilder,private dbService: DbAccessServiceService,private router: Router ) {
    // Initialize form group with validation
    this.frmGroup = this.fb.group({
      email: ['', [Validators.required]], // Validate email
      password: ['', [Validators.required]], // Validate password
    });
}


onSubmit(): void {
  if (this.frmGroup.valid) {
    const email = this.frmGroup.controls['email'].value; // Extract email
    const password = this.frmGroup.controls['password'].value; // Extract password

    // Fetch users from backend
    this.dbService.getUsers().subscribe({
      next: (res) => {
        const allUsers = <UserInfo[]>res; // Typecast response to UserInfo array
        console.log('All users:', allUsers);

        // Filter user based on email and password
        const matchedUser = allUsers.find(
          (user) => user.email === email && user.password === password
        );

        if (matchedUser) {
          console.log('Login successful for:', matchedUser);

          // Store logged-in user data in session storage
          sessionStorage.setItem(
            'LoggedInUserData',
            JSON.stringify(matchedUser)
          );

          // Navigate based on user role
          switch (matchedUser.role.toUpperCase()) {
            case 'ADMIN':
              console.log('Navigating to Admin Dashboard...');
              this.router.navigate(['admindashboard']);
              break;
            case 'PM':
              console.log('Navigating to PM Dashboard...');
              this.router.navigate(['pmdashboard']);
              break;
            case 'MEMBER':
              console.log('Navigating to Member Dashboard...');
              this.router.navigate(['memberdashboard']);
              break;
            default:
              alert('Role not recognized!');
              break;
          }
        } else {
          alert('Invalid email or password!');
        }
      },
      error: (err) => {
        console.error('Error fetching users:', err);
      },
    });
  } else {
    alert('Please fill in all required fields correctly.');
  }
}
}
