import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { CoreModule } from '../../core/core.module';
import { SharedModule } from '../../shared/shared.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PanelRoutingModule } from './panel-routing.module';
import { PanelComponent } from './panel.component';
import { ProductComponent } from './product/product.component';
import { ProfileComponent } from './profile/profile.component';


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
    CoreModule
  ],
  providers: []
})
export default class PanelModule { }
