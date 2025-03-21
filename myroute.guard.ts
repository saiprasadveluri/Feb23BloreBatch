// import { CanActivateFn } from '@angular/router';
// import { UserInfo } from './user-info';
 
// export const myrouteGuard: CanActivateFn = (route, state) => {
//   var reqRole:string[]=route.data['reqRole'];
//   if(sessionStorage.getItem('LoggedinUserData')!=null)
//   {
//     var LoggedinUser:UserInfo=JSON.parse(sessionStorage.getItem('LoggedinUserData')as string);
//     console.log(LoggedinUser);
//     console.log(reqRole);
//     var Found=reqRole.find(str=>str.toUpperCase()==LoggedinUser.role.toUpperCase());
//     if(Found!=undefined)
//     {
//       return true;
//     }
//   }
//   sessionStorage.removeItem('LoggedinUserData');
//   location.href='login';
//   return false;
 
 
// };
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class myrouteGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const requiredRoles = route.data['reqRole'] as string[];
    const userRole = this.authService.getLoggedInUserRole();

    if (this.authService.isLoggedIn() && requiredRoles.includes(userRole)) {
      return true;
    }

    this.router.navigate(['login']);
    return false;
  }
}