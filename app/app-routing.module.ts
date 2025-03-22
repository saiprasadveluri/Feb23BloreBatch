import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { myrouteGuard } from './myroute.guard';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { CreateTaskComponent } from './create-task/create-task.component';
import { ListTasksComponent } from './list-tasks/list-tasks.component';
import { AddMembersComponent } from './add-members/add-members.component';

const routes: Routes = [
  {path:"login",component:AccountComponent},
  {path:"admindashboard",component:AdminDashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['ADMIN']}},
  {path:"memberdashboard",component:MemberDashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['DEVELOPER','QA']}},
  {path:"pmdashboard",component:PmDashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['PM']}},
  {path:"create-task",component:CreateTaskComponent},
  {path:"list-tasks",component:ListTasksComponent},
  {path:"addnewuser",component:AddNewUserComponent},
  {path:"addnewproject",component:AddNewProjectComponent},
  {path:"add-members",component:AddMembersComponent},
  {path:"",redirectTo:"login",pathMatch:"full"},
  {path:"",redirectTo:"pm-dashboard",pathMatch:"full"}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
