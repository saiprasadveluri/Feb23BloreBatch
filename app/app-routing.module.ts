import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { myrouteGuard } from './myroute.guard';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { AssignProjectComponent } from './assign-project/assign-project.component';

const routes: Routes = [
  {path:'login',component:AccountComponent},
  {path:'admindashboard',component:AdminDashboardComponent,canActivate:[myrouteGuard],data:{reqrole:['ADMIN']}},
  {path:'pmdashboard',component:PmDashboardComponent,canActivate:[myrouteGuard],data:{reqrole:['PM']}},
  {path:'memberdashboard',component:MemberDashboardComponent,canActivate:[myrouteGuard],data:{reqrole:['DEV','QA']}},
  {path:'addnewuser',component:AddNewUserComponent},
  {path:'addnewproject',component:AddNewProjectComponent},
  {path:'assignprojectmember',component:AssignProjectComponent},
  {path:'',redirectTo:'login',pathMatch:'full'}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
