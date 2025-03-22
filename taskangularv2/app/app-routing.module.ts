import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { myrouteGuard } from './myroute.guard';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { ManageTasksComponent } from './manage-tasks/manage-tasks.component';
const routes: Routes = [
  // {path:'login',component:AccountComponent},
  // {path:"admindashboard",component:AdminDashboardComponent},
  // {path:'pmdashboard',component:PmDashboardComponent},
  // {path:'memberdashboard',component:MemberDashboardComponent},
  // {path:'addnewuser',component:AddNewUserComponent},
  // {path:'',redirectTo:'/login',pathMatch:'full'}
  {path:"login",component:AccountComponent},
  {path:"admindashboard",component:AdminDashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['ADMIN']}},
  {path:"pmdashboard",component:PmDashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['PM']}},
  {path:"memberdashboard",component:MemberDashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['DEV','QA']}},
  {path:'addnewuser',component:AddNewUserComponent},
  {path:'addnewproject',component:AddNewProjectComponent},
  {path:'managetask/:id',component:ManageTasksComponent},
  {path:"",redirectTo:"login",pathMatch:"full"}
];
 
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }