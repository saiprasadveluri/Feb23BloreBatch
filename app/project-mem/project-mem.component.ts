import { Component,OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Projmember } from '../projmember';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DbAccessService } from '../db-access.service';
import { UserInfo } from '../user-info';

@Component({
  selector: 'app-project-mem',
  templateUrl: './project-mem.component.html',
  styleUrls: ['./project-mem.component.css']
})
export class ProjectMemComponent {
  members: Projmember[] = [];
  allmemb:UserInfo[]=[];
  dmem:UserInfo[] = [];
  qmem:UserInfo[] = [];
  projectId: string | null = null;
  frmGroup: FormGroup;

  constructor(private fb: FormBuilder, private srv: DbAccessService, private router: Router) {
    this.frmGroup = fb.group({
      memId: new FormControl('', [Validators.required]),
    });
  }

  ngOnInit(): void {
    const project = JSON.parse(sessionStorage.getItem('Project') || '{}');
    this.projectId = project || null;
    if (this.projectId) {
      this.srv.GetProjectMembersByProjectId(this.projectId).subscribe({
        next: (res) => {
          this.members = <Projmember[]>res;
        },
        error: (err) => {
          console.error('Error fetching members:', err);
        }
      });
    }
    this.srv.GetAllUsers().subscribe({
      next: (res) => {
        this.allmemb=<UserInfo[]>res;
        this.dmem=this.allmemb.filter(m=>m.role.toUpperCase()==='DEVELOPER');
        this.qmem=this.allmemb.filter(q=>q.role.toUpperCase()==='QA');
      },
      error: (err) => {
        console.error('Error fetching members:', err);
      }
    });
  }

  RefreshView(): void {
    if (this.projectId) {
      this.srv.GetProjectMembersByProjectId(this.projectId).subscribe({
        next: (res) => {
          this.members = <Projmember[]>res;
          console.log('Members:', this.members);
        },
        error: (err) => {
          console.error('Error fetching members:', err);
        }
      });
    }
  }

  Assign(): void {
    var memId = this.frmGroup.controls['memId'].value;
    var projId = this.projectId;
    var newObj: Projmember = {
      projId: projId as string,
      memId: memId,
    };
    this.srv.AddNewProjectMember(newObj).subscribe({
      next: (res) => {
        console.log('New Member Added: ' + res.id);
        this.RefreshView();
      },
      error: (err) => {
        console.error('Error adding member:', err);
      }
    });
  }
}
