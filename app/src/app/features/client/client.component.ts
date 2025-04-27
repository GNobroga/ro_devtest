import { Component, computed, OnInit } from '@angular/core';
import { ProductService } from '../panel/product/product.service';
import { Product } from '../panel/product/product.model';
import { BaseListComponent } from '../../core/components/base-list.component';
import { last, mergeAll, of, takeUntil } from 'rxjs';
import { Filter } from '../../core/models/filter.model';
import { MenuItem } from 'primeng/api';
import { CartItem, CartService } from './services/cart.service';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-client',
  standalone: false,
  templateUrl: './client.component.html',
  styleUrl: './client.component.scss'
})
export class ClientComponent implements OnInit {

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

  cartItems = computed(() => this.cartService.items());

  cartVisible = false;

  constructor(readonly cartService: CartService, readonly authService: AuthService) {}
  
  ngOnInit(): void {
    this.cartService.cartItems$.subscribe(cartItems => {
        this.cartVisible = cartItems.length > 0;
    });
  }
}
