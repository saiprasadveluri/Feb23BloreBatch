import { CanActivateFn } from '@angular/router';
import { UserInfo } from './user-info';
 
export const myrouteGuard: CanActivateFn = (route, state) => {
  var reqRole: string[] = route.data['reqRole'];
  if(sessionStorage.getItem('LoggedinUserData') as string)
  {
    var LoggedInUser:UserInfo=JSON.parse(sessionStorage.getItem('LoggedinUserData')as string);
  console.log(LoggedInUser);
console.log(reqRole);
var Found=reqRole.find(str=>str.toUpperCase()==LoggedInUser.role.toUpperCase());
if(Found!=undefined)
{
  return true;
}
  }
  sessionStorage.removeItem('LoggedinUserData');
  location.href='login';
 
  return false;
};
 
 