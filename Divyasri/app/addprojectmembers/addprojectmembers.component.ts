import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Projectmembers } from '../projectmembers';
;

@Component({
  selector: 'app-addprojectmembers',
  templateUrl: './addprojectmembers.component.html',
  styleUrls: ['./addprojectmembers.component.css']
})
export class AddprojectmembersComponent {

  frmGroup: FormGroup;
  availableUsers: any[] = []; // List of available users

  constructor(private fb: FormBuilder, private srv: DbAccessService, private router: Router) {
    this.frmGroup = this.fb.group({
      projid: ['', Validators.required],
      memid: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadAvailableUsers();
  }

  loadAvailableUsers(): void {
    this.srv.getUsersByRole('DEV').subscribe({
      next: (res) => {
        this.availableUsers = res;
      },
      error: (err) => {
        console.error('Error fetching available users:', err);
      }
    });
  }

  onAddClick(): void {
    if (this.frmGroup.valid) {
      const projid = this.frmGroup.controls['projid'].value;
      const memid = this.frmGroup.controls['memid'].value;

      this.srv.addProjectMember(projid, memid).subscribe({
        next: () => {
          console.log('Member added successfully');
          this.router.navigate(['pm-dashboard']);
        },
        error: (err) => {
          console.error('Error adding member:', err);
        }
      });
    }
  }
}

  
