import { CanActivateFn } from '@angular/router';
import {UserInfo} from './user-info';

export const myrouteGuard: CanActivateFn = (route, state) => 
  {
  var reqRole:string[]=route.data['reqRole'];
  if(sessionStorage.getItem('LoggedinUserData')!=null)
  {
    var LoggedInuser:UserInfo=JSON.parse(sessionStorage.getItem('LoggedinUserData') as string);
    console.log(LoggedInuser) 
    console.log(reqRole);
    var Found=reqRole.find(str=>str.toUpperCase()==LoggedInuser.role.toUpperCase());
    if(Found!=undefined)
    {
      return true;
     
    }
  }
    sessionStorage.removeItem('LoggedinUserData');
    location.href='login';
    return false;
  
};
