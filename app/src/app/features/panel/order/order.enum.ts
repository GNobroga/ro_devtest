
export enum OrderStatus  {
    ALL = 0,
    CANCELLED = 1,
    PAID = 2,
    PENDING = 3,
}

export const OrderStatusUtils = {
    parse(status: 'Cancelled' | 'Paid' | 'Pending'): OrderStatus {
        const key = status.toUpperCase() as keyof typeof OrderStatus;
        const parsed = OrderStatus[key];
        if (parsed === undefined) {
            throw new Error(`Invalid status: ${status}`);
        }
        return parsed;
    }
};