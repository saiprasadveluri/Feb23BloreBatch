import { Component } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
//import { UserInfo } from '../user-info';
import { DBAccessService } from '../dbaccess.service';
import { Router } from '@angular/router';
//import { ProjectInfo } from '../project-info';
import { ProjectInfo } from '../project-info';
@Component({
  selector: 'app-addnewproject',
  templateUrl: './addnewproject.component.html',
  styleUrls: ['./addnewproject.component.css']
})
export class AddnewprojectComponent {
  frmGroup:FormGroup;
constructor(private fb:FormBuilder,private srv:DBAccessService,private router:Router) {
  this.frmGroup = this.fb.group({
    name: new FormControl('', [Validators.required]),
    pm: new FormControl('', [Validators.required]),
});
}
OnAddClick()
{
  var uname = this.frmGroup.controls['name'].value;
  var upm = this.frmGroup.controls['pm'].value;
  var unewObj:ProjectInfo={
    name:uname,
    pm:upm
  };
  this.srv. AddNewProject(unewObj).subscribe({
    next:(res)=>{
      console.log("User added successfully"+res.id);
      this.router.navigate(['admindashboard']);
    },
    error: (err) => {
      console.error("Error adding project:", err); // Log the error
    }
    
}
  );
}

}
