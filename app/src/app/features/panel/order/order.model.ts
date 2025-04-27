import { OrderStatus } from "./order.enum";

export interface OrderSummaryProduct {
    id: string;
    name: string;
    total: number;
}

export interface OrderSummary {
    totalOrders: number;
    totalClients: number;
    revenue: number;
    products: OrderSummaryProduct[];
}

export interface OrderItem {
    id: string;
    name: string;
    price: number;
    quantity: number;
}

export interface OrderUser {
    id: string;
    name: string; 
    email: string;
}

export interface Order {
    id: string;
    status: 'Pending' | 'Cancelled' | 'Paid';
    placedAt: Date;
    total: number;
    user: OrderUser;
    items: OrderItem[];
    modifiedOn: Date;
}

export interface DeleteOrderResult {
    orderId: string;
    deleted: boolean;
}

export interface ChangeOrderStatusResult {
    newStatus: OrderStatus;
}

export interface OrderItemCreateOrUpdate {
    productId: string;
    quantity: number;
}

export interface CreateOrUpdateOrder {
    items: OrderItemCreateOrUpdate[];
}