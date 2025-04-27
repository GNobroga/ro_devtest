import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientComponent } from './client.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'store',
  },
  {
    path: '',
    component: ClientComponent,
    children: [
      {
        path: 'store',
        loadChildren: () => import('./store/store.module'),
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientRoutingModule { }
