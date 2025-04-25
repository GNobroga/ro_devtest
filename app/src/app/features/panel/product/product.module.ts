import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductRoutingModule } from './product-routing.module';
import { ProductService } from './product.service';
import { ProductEditorComponent } from './product-editor/product-editor.component';
import { SharedModule } from '../../../shared/shared.module';
import { ConfirmationService, MessageService } from 'primeng/api';


@NgModule({
  declarations: [
    ProductEditorComponent
  ],
  imports: [
    CommonModule,
    ProductRoutingModule,
    SharedModule
  ],
  providers: [
    ProductService,
    ConfirmationService,
    MessageService
  ]
})
export default class ProductModule { }
