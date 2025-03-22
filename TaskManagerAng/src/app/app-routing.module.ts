import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { myrouteGuard } from './myroute.guard';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { AddProjectMemberComponent } from './add-project-member/add-project-member.component';
import { AddTaskComponent } from './add-task/add-task.component';
import { AddCommentComponent } from './add-comment/add-comment.component';
import { MemberUpdateTaskComponent } from './member-update-task/member-update-task.component';

const routes: Routes = [
  {path:'login',component:AccountComponent},
  {path:'admindashboard',component:AdminDashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['ADMIN']}},
  {path:'pmdashboard',component:PmDashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['PMANAGER']}},
  {path:'memberdashboard',component:MemberDashboardComponent,canActivate:[myrouteGuard],data:{reqRole:['DEVELOPER','QA']}},
  {path:'addnewuser',component:AddNewUserComponent},
  {path:'addnewproject',component:AddNewProjectComponent},
  {path:'addprojmem',component:AddProjectMemberComponent},
  {path:'addtask',component:AddTaskComponent},
  {path:'addcomment',component:AddCommentComponent},
  {path:'updatetask',component:MemberUpdateTaskComponent},
  {path:'',redirectTo:'login',pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
