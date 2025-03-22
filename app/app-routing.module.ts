import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { ViewUsersComponent } from './view-users/view-users.component';
import { ViewProjectsComponent } from './view-projects/view-projects.component';
import { ViewOwnProjectsComponent } from './view-own-projects/view-own-projects.component';
import { AssignMemberComponent } from './assign-member/assign-member.component';
import { CreateTaskComponent } from './create-task/create-task.component';
import { ViewTaskComponent } from './view-task/view-task.component';
import { QaDashboardComponent } from './qa-dashboard/qa-dashboard.component';
import { DeveloperDashboardComponent } from './developer-dashboard/developer-dashboard.component';
import { ViewOwnTaskComponent } from './view-own-task/view-own-task.component';
import { ReassignTaskComponent } from './reassign-task/reassign-task.component';
import { ViewCommentComponent } from './view-comment/view-comment.component';
import { AddCommentComponent } from './add-comment/add-comment.component';

const routes: Routes = [
  {path:"login",component:AccountComponent},
  {path:"admindashboard",component:AdminDashboardComponent},
  {path:"memberdashboard",component:MemberDashboardComponent},
  {path:"pmdashboard",component:PmDashboardComponent},
  {path:"qadashboard",component:QaDashboardComponent},
  {path:"developerdashboard",component:DeveloperDashboardComponent},
  {path:'addnewuser',component:AddNewUserComponent},
  {path:'addnewproject',component:AddNewProjectComponent},
  {path:'viewusers',component:ViewUsersComponent},
  {path:'viewprojects',component:ViewProjectsComponent},
  {path:'viewownprojects', component:ViewOwnProjectsComponent},
  {path:'assignmember', component:AssignMemberComponent},
  {path:'createtask', component:CreateTaskComponent},
  {path:'viewtask', component:ViewTaskComponent},
  {path:'viewowntask', component:ViewOwnTaskComponent},
  {path:'reassigntask', component:ReassignTaskComponent},
  {path:'viewcomment', component:ViewCommentComponent},
  {path:'addcomment', component:AddCommentComponent},

  {path:'',redirectTo:"login",pathMatch:"full"}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
