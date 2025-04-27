import { CanActivateFn } from '@angular/router';

export const UserRoles = {
  Admin: 'Admin',
  Customer: 'Customer'
};

// export const byRole = (role: string ) =

export const roleGuard: CanActivateFn = (route, state) => {
  return true;
};
