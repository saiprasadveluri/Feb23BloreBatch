import { Component } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { Project } from '../project';
import { UserInfo } from '../user-info';
import { Route } from '@angular/router';

@Component({
  selector: 'app-addnewproject',
  standalone: false,
  templateUrl: './addnewproject.component.html',
  styleUrl: './addnewproject.component.css'
})
export class AddnewprojectComponent {
  pmList:UserInfo[]=[];
  frmGroup:FormGroup;
  constructor(private fb:FormBuilder, private srv:DbAccessService, private router :Router ){ this.frmGroup=fb.group({ 
    uname:new FormControl('', [Validators.required]), 
    upm:new FormControl('', [Validators.required]),
    
  })
 }

 OnAddClick(){
  var uname = this.frmGroup.controls['uname'].value;
  var upm = this.frmGroup.controls['upm'].value;
  
  console.log('Name:', uname);
  console.log('PM:', upm);
  

  console.log('Form valid:', this.frmGroup.valid); // Logs true or false
  

  var newObj:Project={
    name:uname,
    pm:upm,
    

  };
  console.log(newObj);

  this.srv.AddNewProject(newObj).subscribe({
    next:(res)=>{
    console.log("New Project Added"+res.id);
    this.router.navigate(['admindashboard']);
    },
    error:(err)=>{

    }
  })
  

  
 }
  

}
