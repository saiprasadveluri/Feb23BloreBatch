import { NgModule } from '@angular/core';
import { mapToCanActivate, RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { PmDashboardComponent } from './pm-dashboard/pm-dashboard.component';
import { MemberDashboardComponent } from './member-dashboard/member-dashboard.component';
import { AddNewUserComponent } from './add-new-user/add-new-user.component';
import { myrouteGuard } from './myroute.guard';
import { AddNewProjectComponent } from './add-new-project/add-new-project.component';
import { DeleteUserComponent } from './delete-user/delete-user.component';
import { ViewProjectsComponent } from './view-projects/view-projects.component';
import { ViewUsersComponent } from './view-users/view-users.component';
import { AddProjectComponent } from './add-project/add-project.component';
import { ViewProjectMembersComponent } from './view-project-members/view-project-members.component';
import { AssignMembersComponent } from './assign-members/assign-members.component';
import { RemoveMembersComponent } from './remove-members/remove-members.component';
import { CreateTaskComponent } from './create-task/create-task.component';
import { ViewTasksComponent } from './view-tasks/view-tasks.component';
 
const routes: Routes = [
  {path:"login",component:AccountComponent},
  {path:'admindashboard' ,component:AdminDashboardComponent,
    canActivate:[myrouteGuard],data:{reqRole:['ADMIN']}},
  {path:'pmdashboard' ,component:PmDashboardComponent,
    canActivate:[myrouteGuard],data:{reqRole:['PM']}},
  {path:'memberdashboard' ,component:MemberDashboardComponent,
    canActivate:[myrouteGuard],data:{reqRole:['DEV','QA']}},
  {path:'addnewuser',component:AddNewUserComponent},
  {path:'addnewproject',component:AddNewProjectComponent},
  { path: 'deleteuser', component: DeleteUserComponent },
  { path: 'view-users', component: ViewUsersComponent },
  { path: 'add-project', component: AddProjectComponent },
  { path: 'view-projects', component: ViewProjectsComponent },
  { path: 'view-my-projects', component: ViewProjectsComponent },
  { path: 'view-project-members', component: ViewProjectMembersComponent },
  { path: 'assign-members', component: AssignMembersComponent },
  { path: 'remove-members', component: RemoveMembersComponent },
  { path: 'create-task', component: CreateTaskComponent },
  { path: 'view-tasks', component: ViewTasksComponent },
  {path:'',redirectTo:'login',pathMatch:'full'}
];
 
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }