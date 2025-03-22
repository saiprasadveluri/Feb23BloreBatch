import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountsComponent } from './accounts/accounts.component';
import { AdminDashBoardComponent } from './admin-dash-board/admin-dash-board.component';
import { PmDashBoardComponent } from './pm-dash-board/pm-dash-board.component';
import { MemberDashBoardComponent } from './member-dash-board/member-dash-board.component';
import { AddnewuserComponent } from './addnewuser/addnewuser.component';
import { myrouteGuard } from './myroute.guard';
import { AddnewprojectComponent } from './addnewproject/addnewproject.component';
import { AssignmembersComponent } from './assignmembers/assignmembers.component';
import { AddnewtaskComponent
  
 } from './addnewtask/addnewtask.component';

const routes: Routes = [
  {path:"login", component:AccountsComponent},
  {path:'admindashboard', component:AdminDashBoardComponent, canActivate:[myrouteGuard],data:{reqRole:['ADMIN']}},
  {path:'pmdashboard', component:PmDashBoardComponent,canActivate:[myrouteGuard],data:{reqRole:['PM']}},
  {path:'memberdashboard', component:MemberDashBoardComponent,canActivate:[myrouteGuard],data:{reqRole:['DEV', 'QA']}},
  {path:'addnewuser', component:AddnewuserComponent},
  {path:'addnewproject', component:AddnewprojectComponent},
  {path:'assignmembers/:projectId', component:AssignmembersComponent},
  {path:'addnewtask/:projectId', component:AddnewtaskComponent},
  {path:'', redirectTo:'login', pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
