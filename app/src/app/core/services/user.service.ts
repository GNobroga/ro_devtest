import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { CreateUser } from "../models/user.model";
import { MessageService } from "primeng/api";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class UserService extends BaseService {
    constructor() {
        super('user');
    }

    createUser(user: CreateUser, role: 'Admin' | 'Customer') {
        return role === 'Admin' ? this.createAdmin(user) : this.createCustomer(user);
    }

    createCustomer(user: CreateUser) {
        return this.handleRequest(
            this.httpClient.post(this.extendApiUrl('/customer'), user)
        );
    }

    createAdmin(user: CreateUser) {
        return this.handleRequest(
            this.httpClient.post(this.extendApiUrl('/admin'), user)
        );
    }
}