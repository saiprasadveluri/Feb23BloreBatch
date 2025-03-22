import { NgModule } from '@angular/core';
import { mapToCanActivate, RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AdmindashboardComponent } from './admindashboard/admindashboard.component';
import { MemberdashboardComponent } from './memberdashboard/memberdashboard.component';
import { PmdashboardComponent } from './pmdashboard/pmdashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { myrouteGuard } from './myroute.guard';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { TaskManagementComponent } from './task-management/task-management.component';

const routes: Routes = [
  {path:"login",component:AccountComponent},
  {path:"admindashboard",component:AdmindashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['ADMIN']}},
  {path:"pmdashboard",component:PmdashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['PM']}},
  {path:"memberdashboard",component:MemberdashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['DEV','QA']}},
  {path:'addnewuser',component:AddNewUserComponent},
  {path:'addnewproject',component:AddNewProjectComponent},
  { path: 'taskmanagement/:projid', component: TaskManagementComponent },
  {path:"",redirectTo:"login",pathMatch:"full"},
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
