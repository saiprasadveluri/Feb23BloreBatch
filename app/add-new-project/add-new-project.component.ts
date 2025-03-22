import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Project } from '../project';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-add-new-project',
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})
export class AddNewProjectComponent {
  users: UserInfo[] = [];
  pmUsers: UserInfo[] = [];
frmGroup:FormGroup;
  constructor(private fb:FormBuilder, private srv:DbAccessService, private router:Router) {
    this.frmGroup=fb.group({
      name:new FormControl('',[Validators.required]),
      pm:new FormControl('',[Validators.required]),
    });
  }

  ngOnInit(): void {
    this.srv.GetAllUsers().subscribe({
      next: (res) => {
        this.users = <UserInfo[]>res;
        this.pmUsers = this.users.filter(user => user.role.toUpperCase() === 'PM');
      },
      error: (err) => {
        console.error("Error fetching users", err);
      }
    });
  }

OnAddProjClick()
  {
    var pname=this.frmGroup.controls['name'].value;
    var ppm=this.frmGroup.controls['pm'].value;
    var newObj:Project={
      name:pname,
      pm:ppm,
    };
    this.srv.AddNewProject(newObj).subscribe({
      next:(res)=>{
        console.log("New Project Added: "+res.id);
        this.router.navigate(['admindashboard']);
      }
  });
}
}