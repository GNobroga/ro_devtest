import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { SharedModule } from '../../shared/shared.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PanelRoutingModule } from './panel-routing.module';
import { PanelComponent } from './panel.component';
import { ProductComponent } from './product/product.component';
import { ProfileComponent } from './profile/profile.component';
import { OrderModule } from './order/order.module';


@NgModule({
  declarations: [
    DashboardComponent,
    PanelComponent,
    ProfileComponent,
    ProductComponent
  ],
  imports: [
    CommonModule,
    PanelRoutingModule,
    SharedModule,
    OrderModule
  ],
  providers: []
})
export default class PanelModule { }
