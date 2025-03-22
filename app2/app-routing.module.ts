import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AccountComponent } from './account/account.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { ManageTaskComponent } from './manage-task/manage-task.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { myrouteGuard } from './myroute.guard';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { ProjectsListComponent } from './projects-list/projects-list.component';

const routes: Routes = [{path:"login",component:AccountComponent},
  //{path:'',redirectTo:'login',pathMatch:'full'},
  {path:'admindashboard',component:AdminDashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['ADMIN']}},
  {path:'pmdashboard',component:PmDashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['PM']}},
  {path:'memberdashboard',component:MemberDashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['DEV','QA']}},
  {path:'addnewuser',component:AddNewUserComponent},
  {path:'addnewproject',component:AddNewProjectComponent},
  {path:'projectslist',component:ProjectsListComponent},
  {path:'managetask',component:ManageTaskComponent},
  {path:'',redirectTo:'login',pathMatch:'full'}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
