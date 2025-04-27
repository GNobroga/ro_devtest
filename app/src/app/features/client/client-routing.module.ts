import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientComponent } from './client.component';
import { StoreComponent } from './store/store.component';
import { OrderHistoryComponent } from './order-history/order-history.component';

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
        component: StoreComponent,
      },
      {
        path: 'order-history',
        component: OrderHistoryComponent,
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientRoutingModule { }
