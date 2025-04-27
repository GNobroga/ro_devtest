import { Component, OnInit } from '@angular/core';
import { BaseListComponent } from '../../../core/components/base-list.component';
import { ProductService } from '../../panel/product/product.service';
import { mergeAll, of, takeUntil } from 'rxjs';
import { Filter } from '../../../core/models/filter.model';
import { Product } from '../../panel/product/product.model';
import { CartService } from '../services/cart.service';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-store',
  standalone: false,
  templateUrl: './store.component.html',
  styleUrl: './store.component.scss'
})
export class StoreComponent extends BaseListComponent<Product> implements OnInit {
  selectedProduct?: Product;
  selectedQuantity = 1;
  productDialogVisible = false;

  breadcrumbs: MenuItem[] = [
      {
        label: 'Home',
        icon: 'pi pi-home',
        routerLink: '/client'
      }
  ];
  
  constructor(
    readonly productService: ProductService, 
    readonly cartService: CartService
  ) {
    super();
  }

  openProductModal(product: Product): void {
    this.selectedProduct = product;
    this.selectedQuantity = 1;
    this.productDialogVisible = true;
  }

  addToCart(product: Product): void {
    this.cartService.addItem({
      productId: product.id,
      name: product.name,
      price: product.price,
      imageUrl: product.imageUrl,
      quantity: this.selectedQuantity
    });
    this.productDialogVisible = false;
  }

  decreaseQuantity(): void {
    if (this.selectedQuantity > 1) {
      this.selectedQuantity--;
    }
  }
  
  increaseQuantity(): void {
    this.selectedQuantity++;
  }

  override ngOnInit(): void {
    super.ngOnInit();

    this.productService.keyword$.pipe(takeUntil(this.destroy$))
      .subscribe(this.setKeyword.bind(this))

    this.productService.triggerListReload$.asObservable()
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.resetKeyword();
        this.loadData();
      });
    
    of(of(this.filters), this.filterChanged$.asObservable().pipe(takeUntil(this.destroy$)))
      .pipe(mergeAll())
      .subscribe(this.loadData.bind(this))
  }

  loadData(filters?: Filter) {
    filters ??= this.filters;
    this.execute(this.productService.list(filters));
  }
}
