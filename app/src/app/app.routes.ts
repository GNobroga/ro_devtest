import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'login',
        loadChildren: () => import('./features/login/login.module'),
    },
    {
        path: 'register',
        loadChildren: () => import('./features/register/register.module'),
    }
];
