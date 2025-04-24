import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrderRoutingModule } from './order-routing.module';
import { OrderService } from './order.service';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    OrderRoutingModule
  ],
  providers: [OrderService]
})
export class OrderModule { }
