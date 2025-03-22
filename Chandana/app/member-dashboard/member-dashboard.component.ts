import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
//import { DataService } from '../data.service';
//import { User } from '../models';
import { UserInfo } from '../user-info';
import { DbAccessService } from '../db-access.service';

@Component({
  selector: 'app-member-dashboard',
  templateUrl: './member-dashboard.component.html',
  styleUrls: ['./member-dashboard.component.css']
})
export class MemberDashboardComponent implements OnInit {
  members: UserInfo[] = [];
  projectId: number | null = null;

  constructor(private route: ActivatedRoute, private dataService: DbAccessService) {}

  ngOnInit(): void {
    this.projectId = +this.route.snapshot.paramMap.get('projectId')!;
    if (this.projectId) {
      this.dataService.getProjectMembers(this.projectId).subscribe((data: UserInfo[]) => {
        this.members = data;
      });
    }
  }
}
