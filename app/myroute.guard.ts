import { CanActivateFn } from '@angular/router';
import { UserInfo } from './user.info';

export const myrouteGuard: CanActivateFn = (route, state) => {
  var reqRole:string[]=route.data['reqRole'];

  if(sessionStorage.getItem('LoggedInUserData')!=null){
    var LoggedInUser:UserInfo=JSON.parse(sessionStorage.getItem('LoggedInUserData') as string);
    console.log(LoggedInUser)
    console.log(reqRole);
    var Found=reqRole.find(str=>str.toUpperCase()==LoggedInUser.role.toUpperCase());
    if(Found!=undefined){
      return true;
    }

  }
  sessionStorage.removeItem(' LoggedInUserData');
  location.href='login';
  return false;
};
//he guard is intended to protect specific routes by ensuring that only users with the required roles can access those routes. If the user's role does not match the required role(s), they will be redirected to the login page.
//PRIVILEDGE OF ESCALATION is a cyberattack technique where an attacker gains unauthorized access to higher privileges by leveraging security flaws, weaknesses, and vulnerabilities in an organization’s system.