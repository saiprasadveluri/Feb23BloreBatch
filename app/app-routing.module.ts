import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AdminDashboardComponent } from './admindashboard/admindashboard.component';
import { MemberdashboardComponent } from './memberdashboard/memberdashboard.component';
import { PmdashboardComponent } from './pmdashboard/pmdashboard.component';
import { AddNewUserComponent } from './addnewuser/addnewuser.component';
import { AddnewprojectComponent } from './addnewproject/addnewproject.component';
import { myrouteGuard } from './myroute.guard';

 
const routes: Routes = [
{path:"login",component:AccountComponent},
{path:'admindashboard',component: AdminDashboardComponent ,canActivate:[myrouteGuard],data:{reqRole:['ADMIN']}},
{path:'pmdashboard',component: PmdashboardComponent , canActivate:[myrouteGuard],data:{reqRole:['PM']}},
{path:'memberdashboard',component: MemberdashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['DEV','QA']}},
{path:'addnewuser',component: AddNewUserComponent},
{path:'addnewproject',component:AddnewprojectComponent},
{path:'',redirectTo:"login",pathMatch:"full"}
];
 
 
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }