import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DbAccessServiceService } from '../db-access-service.service';
import { UserInfo } from '../userinfo';
import { Router } from '@angular/router';

@Component({
  selector: 'app-adduser',
  standalone: false,
  templateUrl: './adduser.component.html',
  styleUrls: ['./adduser.component.css']
})
export class AdduserComponent {
  adduserForm:FormGroup;
  
  constructor(private fb:FormBuilder, private dbService:DbAccessServiceService, private router:Router){
    this.adduserForm=this.fb.group({
      name:['',[Validators.required]],
      email:['',[Validators.required]],
      password:['',[Validators.required]],
      role:['',[Validators.required]],
      
    });
    console.log('youu');
  }

  
  onSubmit(): void {
    if (this.adduserForm.valid) {
      const userData = this.adduserForm.value;
      console.log('user Data:',userData);
      this.dbService.adduser(userData).subscribe({
        next: (response) => {
          console.log('User added successfully:', response);
          this.adduserForm.reset(); // Reset the form after successful submission
        },
        error: (error) => {
          console.error('Error adding user:', error );
        },
      });
    } else {
      console.log('Form is invalid');
      this.adduserForm.markAllAsTouched();
    }
  } 
}
