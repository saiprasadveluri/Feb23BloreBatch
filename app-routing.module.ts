import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { AddNewUseComponent } from './add-new-use/add-new-use.component';
import { myrouteGuard } from './myroute.guard';

const routes: Routes = [
  {path:"login",component:AccountComponent},
  // HERE CANACTIVATE:[MYROUTE GUARD IS BASICALLY USED FOR ROUTING AND PREVENTING A SPECIFIC ROLE TO ANOTHER PERSONS DASHBOARD]
  {path:'admindashboard',component:AdminDashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['ADMIN']}},
  {path:'pmdashboard',component:PmDashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['PM']}},
{path:'memberdashboard',component:MemberDashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['DEV','QA']}},
{path:'addnewuser',component:AddNewUseComponent},
{path:'' ,redirectTo:'login', pathMatch:'full'},
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
