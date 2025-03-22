import { CanActivateFn } from '@angular/router';
import { Userinfo } from './userinfo';

export const myrouteGuard: CanActivateFn = (route, state) => {
  var reqRole:string[]=route.data['reqRole'];
  if(sessionStorage.getItem('LoggedinUser')!=null)
  {
    var LoggedinUser:Userinfo=JSON.parse(sessionStorage.getItem('LoggedinUser') as string);
    console.log(LoggedinUser);
    console.log(reqRole);
    var Found=reqRole.find(str=>str.toUpperCase()==LoggedinUser.role.toUpperCase());
    if(Found!=undefined)
    {
      return true;
    }
  }
  sessionStorage.removeItem('LoddedinUser');
    location.href='login';
    return false;
};