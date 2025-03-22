import { CanActivateFn } from '@angular/router';
import { UserInfo } from './user-info';

// export const myrouteGuard: CanActivateFn = (route, state) => {
//   var reqRole:string[]=route.data['reqRole'];
//   if(sessionStorage.getItem('LoggedInUserData') !=null){
//     var LoggedInUser:UserInfo=JSON.parse(sessionStorage.getItem('LoggedInUserData') as string);
//     console.log(LoggedInUser);
//     console.log(reqRole);
//     var Found = reqRole.find(str=>str.toUpperCase()==LoggedInUser.role.toUpperCase());
//     if(Found!=undefined){
//       return true;
//     }
//   }
//   sessionStorage.removeItem('LoggedInUserData');
//   location.href='login';
//   return false;
// };

export const myrouteGuard: CanActivateFn = (route, state) => {
  const reqRole: string[] = route.data?.['reqRole'];
  if (reqRole && sessionStorage.getItem('LoggedInUserData') !== null) {
    const LoggedInUser: UserInfo = JSON.parse(sessionStorage.getItem('LoggedInUserData') as string);
    console.log(LoggedInUser);
    console.log(reqRole);
    const Found = reqRole.find(str => str.toUpperCase() === LoggedInUser.role.toUpperCase());
    if (Found != undefined) {
      return true;
    }
  }
  sessionStorage.removeItem('LoggedInUserData');
  location.href = 'login';
  return false;
};

