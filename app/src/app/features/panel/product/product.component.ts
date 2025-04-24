import { Component, OnInit } from '@angular/core';
import { BaseListComponent } from '../../../core/components/base-list.component';
import { Product } from './product.model';
import { PaginatorState } from 'primeng/paginator';
import { ProductService } from './product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  providers: [],
  standalone: false,
})
export class ProductComponent extends BaseListComponent<Product>{

    constructor(readonly service: ProductService) {
      super();
    }

    override ngOnInit(): void {
      super.ngOnInit();
      
      this.execute(this.service.list(this.filters));

      this.filterChanged$
        .subscribe(filters => this.execute(this.service.list(filters)));
    }

}