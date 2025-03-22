import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OwnerDashboardComponent } from './owner-dashboard/owner-dashboard.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AccountComponent } from './account/account.component';
import { HomeComponent } from './home/home.component';
import { AppComponent } from './app.component';
import { ManageMenuComponent } from './manage-menu/manage-menu.component';
import { AddnewuserComponent } from './addnewuser/addnewuser.component';
import { AddnewrestaurantComponent } from './addnewrestaurant/addnewrestaurant.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { OrderFoodComponent } from './order-food/order-food.component';
import { ConfirmorderComponent } from './confirmorder/confirmorder.component';

const routes: Routes = 
[
  {path:'account',component:AccountComponent},
  {path:'admindashboard',component:AdminDashboardComponent},
  {path:'ownerdashboard',component:OwnerDashboardComponent},
  {path:'managemenu/:rid',component:ManageMenuComponent},
  {path:'addnewrestaurant',component:AddnewrestaurantComponent}, 
  {path:'userdashboard',component:UserDashboardComponent,
    children:[
      {path:'orderfood/:rid',component:OrderFoodComponent},
      {path:'confirmorder',component:ConfirmorderComponent}
    ]

  }, 
   {path:'',redirectTo:'account',pathMatch:'full'},   
   {path:'**',redirectTo:'account'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
