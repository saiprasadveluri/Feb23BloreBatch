import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DBAccessService } from '../dbaccess.service';
import { ProjectInfo } from '../project-info';
import { Route,Router } from '@angular/router';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-add-new-project',
  standalone:false,
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})
export class AddNewProjectComponent {
  frmGroup: FormGroup;
  Users: UserInfo[] = [];
  selectedUserId: null| number=null;

  constructor(private fb: FormBuilder, private srv: DBAccessService, private router: Router) {
    // Ensure all the required controls are included in the FormGroup
    this.frmGroup = fb.group({
      pname: new FormControl('', [Validators.required]),
      pm: new FormControl('', [Validators.required])
    });
  }

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers() {
    this.srv.GetAllUsers().subscribe(
      (users) => {
        this.Users = users;
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    );
  }
 
  OnAddProject() {
      const pname = this.frmGroup.controls['pname'].value;
      const pm = this.selectedUserId;
 
      const newObj: ProjectInfo = {
        pname: pname,
        pm: pm !== null ? pm.toString() : '',
      };
 
      this.srv.AddNewProject(newObj).subscribe({
        next: (res) => {
          console.log('New Project Added: ' + res.id);
          this.router.navigate(['admindashboard']);
        },
        error: (err) => {
          console.error('Error adding project:', err);
        }
      });
   
  }
}
 