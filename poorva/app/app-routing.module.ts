import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { myrouteGuard } from './myroute.guard';
import { AddProjectComponent } from './add-project/add-project.component';

import { ViewUsersComponent } from './view-users/view-users.component';
import { ViewProjectsComponent } from './view-projects/view-projects.component';
import { ViewOwnProjectsComponent } from './view-own-projects/view-own-projects.component';
const routes: Routes = [
  { path: "login", component: AccountComponent },
  { path: "admindashboard", component: AdminDashboardComponent, canActivate: [myrouteGuard], data: { reqRole: ['ADMIN'] } },
  { path: "pmdashboard", component: PmDashboardComponent, canActivate: [myrouteGuard], data: { reqRole: ['PM'] } },
  { path: "memberdashboard", component: MemberDashboardComponent, canActivate: [myrouteGuard], data: { reqRole: ['DEV', 'QA'] } },
  { path: "addnewuser", component: AddNewUserComponent },
  { path: "addnewproject", component: AddProjectComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  {path:"viewusers",component:ViewUsersComponent},
  {path:"viewprojects",component:ViewProjectsComponent},
  {path:"viewownprojects",component:ViewOwnProjectsComponent},
  {path:"",redirectTo:'/pmdashboard',pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }