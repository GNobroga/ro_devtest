import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClientRoutingModule } from './client-routing.module';
import { ClientComponent } from './client.component';
import { SharedModule } from '../../shared/shared.module';
import ProductModule from '../panel/product/product.module';
import { OrderHistoryComponent } from './order-history/order-history.component';
import { StoreComponent } from './store/store.component';
import { OrderDetailsComponent } from './order-details/order-details.component';


@NgModule({
  declarations: [
    ClientComponent,
    OrderHistoryComponent,
    StoreComponent,
    OrderDetailsComponent
  ],
  imports: [
    CommonModule,
    ClientRoutingModule,
    SharedModule,
    ProductModule
  ]
})
export default class ClientModule { }
