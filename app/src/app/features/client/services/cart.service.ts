import { Injectable, signal } from "@angular/core";
import { Product } from "../../panel/product/product.model";
import { BehaviorSubject } from "rxjs";
import { BaseService } from "../../../core/services/base.service";
import { OrderService } from "../../panel/order/order.service";
import { OrderItem, OrderItemCreateOrUpdate } from "../../panel/order/order.model";
import { ToastrService } from "ngx-toastr";

export interface CartItem {
    productId: string;
    name: string;
    price: number;
    quantity: number;
    imageUrl?: string;
}

@Injectable({
    providedIn: 'root'
})
export class CartService  {

    items = signal<CartItem[]>([]);
    
    private cartItemsSubject = new BehaviorSubject<CartItem[]>([]);
    cartItems$ = this.cartItemsSubject.asObservable();

    constructor(readonly orderService: OrderService, readonly toastrService: ToastrService) {}

    createOrder() {
        const items = this.items().map(({ productId, quantity }) => ({ productId, quantity }) as OrderItemCreateOrUpdate);
        if (items.length > 0) {
            this.orderService.create({ items }).subscribe(() => {
                this.clearCart();
                this.toastrService.success("Seu pedido foi realizado com sucesso");
            });
        }
    }

    addItem(item: CartItem): void {
        const items = this.items();

        const existingItem = items.find(i => i.productId === item.productId);
        if (existingItem) {
            existingItem.quantity += item.quantity;
        } else {
            items.push({ ...item });
        }
    
        this.updateCart(items);
    }

    removeItem(productId: string): void {
        this.updateCart(this.items().filter(i => i.productId !== productId));
      }
    
      updateQuantity(productId: string, quantity: number): void {
        const item = this.items().find(i => i.productId === productId);
    
        if (item) {
          item.quantity = quantity;
          if (item.quantity <= 0) {
            this.removeItem(productId);
          } else {
            this.updateCart();
          }
        }
      }

    clearCart(): void {
        this.items.set([])
        this.updateCart([]);
    }

    getTotalQuantity(): number {
        return this.items().reduce((total, item) => total + item.quantity, 0);
    }

    getTotalPrice(): number {
        return this.items().reduce((total, item) => total + (item.price * item.quantity), 0);
    }
    
    private updateCart(items?: CartItem[]): void {
        const copyCartItems = [...(items ?? this.items())];
        this.items.set(copyCartItems);
        this.cartItemsSubject.next(copyCartItems);
    }
    
}