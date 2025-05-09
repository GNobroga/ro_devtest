import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { ApplicationConfig, LOCALE_ID, provideZoneChangeDetection } from '@angular/core';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideRouter, withComponentInputBinding } from '@angular/router';
import Aura from '@primeng/themes/aura';
import { providePrimeNG } from 'primeng/config';
import { routes } from './app.routes';
import { provideToastr } from 'ngx-toastr';
import localePt from '@angular/common/locales/pt';
import { registerLocaleData } from '@angular/common';
import { jwtInterceptor } from './core/interceptors/jwt.interceptor';
import { AuthService } from './core/services/auth.service';

registerLocaleData(localePt);


export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes, withComponentInputBinding()),
    provideHttpClient(withInterceptors([jwtInterceptor])),
    provideAnimationsAsync(),
    provideToastr(),
    providePrimeNG({
      theme: {
          preset: Aura,
          options: {
            cssLayer: {
                name: 'primeng',
                order: 'tailwind-base, primeng, tailwind-utilities'
            }
        }
      },
    }),
    { provide: LOCALE_ID, useValue: 'pt-BR' }
  ]
};
