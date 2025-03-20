import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';

const routes: Routes = [{path:"login",component:AccountComponent},
  {path:'',redirectTo:'login',pathMatch:'full'},
  {path:'admindashboard',component:AdminDashboardComponent},
  {path:'pmdashboard',component:PmDashboardComponent},
  {path:'memberdashboard',component:MemberDashboardComponent},
  {path:'addnewuser',component:AddNewUserComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
