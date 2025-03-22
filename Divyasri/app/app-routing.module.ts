import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AdmindashboardComponent } from './admindashboard/admindashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';

import { HttpClientModule } from '@angular/common/http'; 
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { myrouteGuard } from './myroute.guard';
import { CreatetaskComponent  } from './createtask/createtask.component';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { AddprojectlistComponent } from './addprojectlist/addprojectlist.component';
import { ManageMembersComponent } from './manage-members/manage-members.component';
import { AddprojectmembersComponent } from './addprojectmembers/addprojectmembers.component';
import { ViewuserlistComponent } from './viewuserlist/viewuserlist.component';
import { PmDashboardGuard } from './pm-dashboard.guard';
import { AddCommentsComponent } from './add-comments/add-comments.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { ViewmemberlistComponent } from './viewmemberlist/viewmemberlist.component';

const routes: Routes = [
  {path:'login',component:AccountComponent},
  {path:'pm-dashboard',component:PmDashboardComponent ,canActivate:[myrouteGuard], data:{reqRole:['PM']}},
    {path:'admindashboard',component:AdmindashboardComponent,canActivate:[myrouteGuard], data:{reqRole:['ADMIN']}},
    {path:'manage-member-dashboard/id',component:ManageMembersComponent,
      canActivate:[myrouteGuard],data:{reqRole:['DEV',"QA"]}
    },
    { path: 'user-dashboard', component: UserDashboardComponent, canActivate: [myrouteGuard], data: { reqRole: ['DEV', 'QA'] } },
    {path:'addcomment',component:AddCommentsComponent},
    { path: 'pm-dashboard', component: PmDashboardComponent, canActivate: [PmDashboardGuard] },
    {path:'viewuserlist',component:ViewuserlistComponent},
    {path:'addnewuser',component:AddNewUserComponent},
    {path:'createtask',component:CreatetaskComponent },
    {path:'addnewproject',component:AddNewProjectComponent},
    { path: 'add-project-members/:id', component: AddprojectmembersComponent }, // Route for adding project members
    {path:'assignto',component:AddprojectlistComponent},
    { path: 'view-members/:id', component: ViewmemberlistComponent },
    { path: '', redirectTo: '/manage-members', pathMatch: 'full' } ,
    {path:'',redirectTo:'login',pathMatch:'full'}
  // Optional: Redirect to Manage Members
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


















