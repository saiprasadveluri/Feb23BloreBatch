import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserlistComponent } from './userlist/userlist.component';
import { UseritemComponent } from './useritem/useritem.component';
import { AdduserComponent } from './adduser/adduser.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AdUserListComponent } from './ad-user-list/ad-user-list.component';
import { AdRestaurantListComponent } from './ad-restaurant-list/ad-restaurant-list.component';

import { HttpClient, HttpClientModule } from '@angular/common/http';
import { MydataService } from './mydata.service';
import { OwnerDashboardComponent } from './owner-dashboard/owner-dashboard.component';
import { OwnerRestaurantListComponent } from './owner-restaurant-list/owner-restaurant-list.component';
import { HighlightDirective } from './highlight.directive';
import { AccountComponent } from './account/account.component';
import { HomeComponent } from './home/home.component';
import { MenuComponent } from './menu/menu.component';
import { RouterOutlet } from '@angular/router';
import { ManageMenuComponent } from './manage-menu/manage-menu.component';
import { AddnewuserComponent } from './addnewuser/addnewuser.component';
import { AddnewrestaurantComponent } from './addnewrestaurant/addnewrestaurant.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { OrderFoodComponent } from './order-food/order-food.component';
import { ConfirmorderComponent } from './confirmorder/confirmorder.component';

@NgModule({
  declarations: [
    AppComponent,
    UserlistComponent,
    UseritemComponent,
    AdduserComponent,
    AdminDashboardComponent,
    AdUserListComponent,
    AdRestaurantListComponent,
    OwnerDashboardComponent,
    OwnerRestaurantListComponent,
    HighlightDirective,
    AccountComponent,
    HomeComponent,
    MenuComponent,
    ManageMenuComponent,
    AddnewuserComponent,
    AddnewrestaurantComponent,
    UserDashboardComponent,
    OrderFoodComponent,
    ConfirmorderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,    
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [MydataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
