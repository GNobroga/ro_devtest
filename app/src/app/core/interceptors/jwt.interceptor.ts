import { HttpHeaders, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);

  const accessToken = authService.accessToken;

  if (authService.isAuthenticated && accessToken) {
    const authorizedRequest = req.clone({
      setHeaders: {
        Authorization: `Bearer ${accessToken}`
      }
    });
    return next(authorizedRequest);
  }

  return next(req);
};