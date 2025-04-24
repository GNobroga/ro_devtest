export interface Product {
    id: string;
    name: string;
    price: number;
    description?: string;
}

export interface CreateProduct {
    name: string;
    price: number;
    description?: string;
}

export interface CreateProductResult {
    id: string;
    name: string;
    price: string; 
    description?: string;
    createdOn: Date;
}

export interface UpdateProduct {
    name: string;
    price: number;
    description?: string;
}

export interface UpdateProductResult {
    id: string;
    name: string;
    price: string;
    description?: string;
    createdOn: Date;
}

export interface DeleteProductResult {
    productId: string;
    isDeleted: boolean;
}

