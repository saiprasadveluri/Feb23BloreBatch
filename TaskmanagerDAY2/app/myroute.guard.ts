import { CanActivateFn } from '@angular/router';
import { Userinfo } from './userinfo';


export const myrouteGuard: CanActivateFn = (route, state) => {
  var reqrole:string[]=route.data['reqrole'];
  if(sessionStorage.getItem('LoggedinUserData')!=null){
    var LoggedinUserData:Userinfo=JSON.parse(sessionStorage.getItem('LoggedinUserData' )as string);
    console.log(LoggedinUserData);
    console.log(reqrole);
    var Found=reqrole.find(str=>str.toUpperCase()==LoggedinUserData.role.toUpperCase());
    if(Found!=undefined){
      return true;
    }
  }
  sessionStorage.removeItem('LoggedinUserData');
  location.href='login';
  return false;
};

