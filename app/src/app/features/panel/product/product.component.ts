import { Component, OnInit } from '@angular/core';
import { BaseListComponent } from '../../../core/components/base-list.component';
import { Product } from './product.model';
import { PaginatorState } from 'primeng/paginator';
import { ProductService } from './product.service';
import { Filter } from '../../../core/models/filter.model';
import { merge, mergeAll, of, takeUntil } from 'rxjs';

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

      this.service.triggerListReload$.asObservable()
        .pipe(takeUntil(this.destroy$))
        .subscribe(() => this.loadData());

      of(of(this.filters), this.filterChanged$.asObservable().pipe(takeUntil(this.destroy$)))
        .pipe(mergeAll())
        .subscribe(this.loadData.bind(this))
    }

    loadData(filters?: Filter) {
      filters ??= this.filters;
      this.execute(this.service.list(filters));
    }

}