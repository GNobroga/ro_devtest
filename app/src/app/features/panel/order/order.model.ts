
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

