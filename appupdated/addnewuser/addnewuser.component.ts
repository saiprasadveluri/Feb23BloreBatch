import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-addnewuser',
  templateUrl: './addnewuser.component.html',
  styleUrls: ['./addnewuser.component.css']
})
export class AddnewuserComponent {
myformGroup:FormGroup;
constructor(private fb:FormBuilder)
{
  this.myformGroup=this.fb.group({
        ctrlName:new FormControl('',[Validators.required]),
        ctrlEmail:new FormControl('',[Validators.required,Validators.email])
  });
}
AddNewUserRecord()
{
  console.log(this.myformGroup.controls['ctrlName'].value);
}
}
