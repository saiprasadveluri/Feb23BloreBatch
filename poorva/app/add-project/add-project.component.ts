import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { ProjectInfo } from '../project-info';

@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.css']
})
export class AddProjectComponent {
  frmGroup:FormGroup;
  constructor(private fb:FormBuilder,private srv:DbAccessService , private router:Router){
    this.frmGroup=fb.group({
      name:new FormControl('',[Validators.required]),
      PM:new FormControl('',[Validators.required]),
     
    });
  }
    OnAddClick(){
      var pname = this.frmGroup.controls['name'].value;
      var ppm = this.frmGroup.controls['PM'].value;
      var newObj:ProjectInfo={
        name:pname,
        pm:ppm
      };
      this.srv.AddNewProject(newObj).subscribe({
        next:(res)=>{
          console.log("New Project Added : "+res.id);
          this.router.navigate(['admindashboard']);
        },
        error:(res)=>{
  
        }
      })
    }
}

