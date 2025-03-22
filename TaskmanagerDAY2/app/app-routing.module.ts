import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { GetUsersComponent } from './get-users/get-users.component';
import { myrouteGuard } from './myroute.guard';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { AssignProjectComponent } from './assign-project/assign-project.component';
import { AssignTaskComponent } from './assign-task/assign-task.component';
import { AddCommentComponent } from './add-comment/add-comment.component';
import { DeveloperPlaceComponent } from './developer-place/developer-place.component';
import { QaPlaceComponent } from './qa-place/qa-place.component';

const routes: Routes = [
{path:'login',component:AccountComponent},
{path:'',redirectTo:'login',pathMatch:'full'},
{path:'admindashboard',component:AdminDashboardComponent,
  canActivate:[myrouteGuard],data:{reqrole:['ADMIN']}},
{path:'memberdashboard',component:MemberDashboardComponent
  ,canActivate:[myrouteGuard],data:{reqrole:['DEV','QA']}
},
{path:'pmdashboard',component:PmDashboardComponent,
  canActivate:[myrouteGuard],data:{reqrole:['PM']}
},
{path:'addnewuser',component:AddNewUserComponent},
{path:'getallusers',component:GetUsersComponent},
{path:'addnewproject',component:AddNewProjectComponent},
{path:'assignproject',component:AssignProjectComponent},
{path:'assigntask',component:AssignTaskComponent},
{path:'addcomment',component:AddCommentComponent},
{ path: 'developerplace',component:DeveloperPlaceComponent },
  { path: 'qaplace', component:QaPlaceComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
