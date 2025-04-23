import { BaseModel } from "../../core/models/base.model";

export interface Product extends BaseModel {
    name: string;
    price: number;
    description?: string;
}