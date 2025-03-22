import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DbAccessService } from '../db-access.service';

@Component({
  selector: 'app-viewmemberlist',
  templateUrl: './viewmemberlist.component.html',
  styleUrls: ['./viewmemberlist.component.css']
})
export class ViewmemberlistComponent {
  projectId: string = '';
  memberList: any[] = [];

  constructor(private route: ActivatedRoute, private srv: DbAccessService) {}

  ngOnInit(): void {
    this.projectId = this.route.snapshot.paramMap.get('id') || '';
    this.loadMembers();
  }

  loadMembers(): void {
    this.srv.getProjectMember(this.projectId).subscribe({
      next: (res) => {
        this.memberList = res;
        console.log('Members assigned to project:', this.memberList);
      },
      error: (err) => {
        console.error('Error fetching members:', err);
      }
    });
  }

}
