import { Component, computed, OnDestroy, OnInit } from '@angular/core';
import { ProductService } from '../panel/product/product.service';
import { Product } from '../panel/product/product.model';
import { BaseListComponent } from '../../core/components/base-list.component';
import { last, mergeAll, of, Subject, takeUntil } from 'rxjs';
import { Filter } from '../../core/models/filter.model';
import { MenuItem } from 'primeng/api';
import { CartItem, CartService } from './services/cart.service';
import { AuthService } from '../../core/services/auth.service';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-client',
  standalone: false,
  templateUrl: './client.component.html',
  styleUrl: './client.component.scss'
})
export class ClientComponent implements OnInit, OnDestroy {

  menus: MenuItem[] = [
    {
      label: 'Meus pedidos',
      icon: 'pi pi-shopping-bag', // ícone de pedidos
      routerLink: '/client/order-history',
    },
    {
      label: 'Sair',
      icon: 'pi pi-sign-out', 
      command: () => this.authService.logout(),
    }
  ];

  keywordControl = new FormControl('');

  cartItems = computed(() => this.cartService.items());

  cartVisible = false;

  destroy$ = new Subject();

  constructor(
    readonly cartService: CartService, 
    readonly authService: AuthService,
    readonly productService: ProductService
  ) {}
  
  ngOnInit(): void {

    this.keywordControl.valueChanges.pipe(takeUntil(this.destroy$))
      .subscribe(value => this.productService.setKeyword(value!));

    this.cartService.cartItems$.subscribe(cartItems => {
        this.cartVisible = cartItems.length > 0;
    });
  }

  ngOnDestroy(): void {
      this.destroy$.next(true);
  }
}
