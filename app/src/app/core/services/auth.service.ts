import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable, tap } from "rxjs";
import { ApiResponse } from "../models/api-response.model";
import { LoginCredentials, TokenInfo, TokenStorage } from "../models/auth.model";
import { BaseService } from "./base.service";

@Injectable()
export class AuthService extends BaseService {

    private readonly TOKEN_KEY = "auth_tokens";

    constructor(readonly router: Router) {
        super("auth");
    }

    login(credentials: LoginCredentials): Observable<ApiResponse<TokenInfo>> {
        return this.handleRequest<TokenInfo>(this.httpClient.post(this.apiUrl, credentials))
            .pipe(tap(resp => {
                const { accessToken, refreshToken } = resp.data as TokenInfo;
                this.setTokenStorage({ accessToken, refreshToken });
                this.router.navigate(['/dashboard']);
            }));
    }

    logout() {
        this.clearTokenStorage();
        this.router.navigate(['/login']);
    }

    getNewAccessToken(refreshToken: string): Observable<ApiResponse<TokenInfo>> {
        return this.handleRequest(this.httpClient.post(this.extendApiUrl("/exchange-token"), {
            refreshToken
        }));
    }

    setTokenStorage(storage: TokenStorage) {
        localStorage.setItem(this.TOKEN_KEY, JSON.stringify(storage));
    }

    getTokenStorage(): TokenStorage | null {
        const storedData = localStorage.getItem(this.TOKEN_KEY);
        return storedData ? JSON.parse(storedData) : null;
    }

    get accessToken() {
        return this.getTokenStorage()?.accessToken;
    }

    get refreshToken() {
        return this.getTokenStorage()?.refreshToken;
    }

    clearTokenStorage() {
        localStorage.removeItem(this.TOKEN_KEY);
    }
}