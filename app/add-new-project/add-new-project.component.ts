import { Component } from '@angular/core';
import { FormGroup,FormBuilder,FormControl , Validators  } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { Router } from '@angular/router';
import { ProjInfo } from '../proj-info';

@Component({
  selector: 'app-add-new-project',
  templateUrl: './add-new-project.component.html',
  styleUrls: ['./add-new-project.component.css']
})

export class AddNewProjectComponent {
  frmGroup:FormGroup;

  // pmList:any[]=[];

  constructor(private fb:FormBuilder,private srv:DbAccessService,private router:Router)
  {
    this.frmGroup=fb.group({
      name:new FormControl('',[Validators.required]),
      PM:new FormControl('',[Validators.required])
    })
  }

//   ngOnInit():void{
//     this.loadPMList();
//   }
// loadPMList():void{
//   this.srv.getPMList().subscribe((data:any[])=>{
//     this.pmList=data;
//   });
// }

  OnAddClick()
  {
    var name=this.frmGroup.controls['name'].value;
    var PM=this.frmGroup.controls['PM'].value;
    var newObj:ProjInfo={
      name:name,
      pm:PM
    };
    this.srv.AddNewProj(newObj).subscribe({
      next:(res)=>{
        console.log("New Project Added"+res.id);
        this.router.navigate(['admindashboard']);
      },
      error:(err)=>{
 
      }
    });
  }
}


