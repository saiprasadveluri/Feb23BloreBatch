import { CanActivateFn } from '@angular/router';

export const myrouteGuard: CanActivateFn = (route, state) => {
  return true;
};
