import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { AddnewprojectComponent } from './addnewproject/addnewproject.component';
import { myrouteGuard } from './myroute.guard';
import { ViewUsersComponent } from './view-users/view-users.component';
import { ViewProjectsComponent } from './view-projects/view-projects.component';
const routes: Routes = [
  
    { path: 'viewusers', component: ViewUsersComponent, canActivate: [myrouteGuard], data: { reqRole: ['ADMIN'] } },
    { path: 'viewprojects', component: ViewProjectsComponent, canActivate: [myrouteGuard], data: { reqRole: ['ADMIN'] } },

  
  { path: 'login', component: AccountComponent },
  { 
    path: 'admindashboard', 
    component: AdminDashboardComponent, 
    canActivate: [myrouteGuard], 
    data: { reqRole: ['ADMIN'] } 
  },
  { 
    path: 'pmdashboard', 
    component: PmDashboardComponent, 
    canActivate: [myrouteGuard], 
    data: { reqRole: ['PM'] } 
  },
  { 
    path: 'memberdashboard', 
    component: MemberDashboardComponent, 
    canActivate: [myrouteGuard], 
    data: { reqRole: ['DEV', 'QA'] } 
  },
  { path: 'addnewuser', component: AddNewUserComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'addnewproject', component: AddnewprojectComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }