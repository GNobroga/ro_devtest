export interface Product {
    id: string;
    name: string;
    price: number;
    imageUrl?: string;
    description?: string;
}

export interface CreateProduct {
    name: string;
    price: number;
    imageUrl?: string;
    description?: string;
}

export interface CreateProductResult {
    id: string;
    name: string;
    price: string; 
    imageUrl?: string;
    description?: string;
    createdOn: Date;
}

export interface UpdateProduct {
    name: string;
    imageUrl?: string;
    price: number;
    description?: string;
}

export interface UpdateProductResult {
    id: string;
    name: string;
    price: string;
    imageUrl?: string;
    description?: string;
}

export interface DeleteProductResult {
    productId: string;
    isDeleted: boolean;
}

