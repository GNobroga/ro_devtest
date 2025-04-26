import { Injectable, signal, Signal, WritableSignal } from "@angular/core";
import { Router } from "@angular/router";
import { lastValueFrom, map, Observable, tap } from "rxjs";
import { jwtDecode } from "jwt-decode";
import { ApiResponse } from "../models/api-response.model";
import { LoginCredentials, TokenInfo, TokenPayload, TokenStorage, User } from "../models/auth.model";
import { BaseService } from "./base.service";
import { UserService } from "./user.service";

@Injectable()
export class AuthService extends BaseService {

    private readonly TOKEN_KEY = "auth_tokens";

    readonly user = signal<User | null>(null);

    constructor(readonly router: Router, readonly userService: UserService) {
        super("auth");
        if (this.isAuthenticated) {
            this.tryToLoadUserProfile();
        }
    }

    async tryToLoadUserProfile() {
        try {
            await lastValueFrom(this.loadUserProfile()!);
        } catch (err) {
            await this.tryToLoadTokenWithRefresh();
        }
       
    }

    async tryToLoadTokenWithRefresh() {
        try {
            if (this.refreshToken == null) {
                throw new Error('refresh token invalido');
            }
            const { accessToken, refreshToken } = await lastValueFrom(this.getNewAccessToken(this.refreshToken));
            this.setTokenStorage({ accessToken, refreshToken });
            this.loadUserProfile()?.subscribe();
        } catch (ex) {
            throw new Error((ex as Error)?.message ?? 'não foi possível carregar o token através do refresh token');
        }
    }
    

    login(credentials: LoginCredentials): Observable<ApiResponse<TokenInfo>> {
        return this.handleRequest<TokenInfo>(this.httpClient.post(this.apiUrl, credentials))
            .pipe(tap(resp => {
                const { accessToken, refreshToken } = resp.data as TokenInfo;
                this.setTokenStorage({ accessToken, refreshToken });
                this.loadUserProfile()?.subscribe(() => {
                    if (this.tokenPayload?.role.includes('Admin')) {
                        this.router.navigate(['/admin/panel']);
                    } else {
                        this.router.navigate(['/shop']);
                    }
                });
            }));
    }

    logout() {
        this.clearTokenStorage();
        this.router.navigate(['/login']);
    }

    getNewAccessToken(refreshToken: string): Observable<TokenInfo> {
        return this.handleRequest<TokenInfo>(this.httpClient.post(this.extendApiUrl("/exchange-token"), {
            refreshToken
        })).pipe(map(r => r.data!));
    }

    get isAuthenticated() {
        return this.accessToken != null;
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

    loadUserProfile() {
        if (!this.isAuthenticated) return;
        const sub = this.tokenPayload?.sub!;
        return this.userService.getById(sub).pipe(tap(response => {
            this.user.set(response.data!);
        }));
    }

    get tokenPayload() {
        if (this.accessToken != null) {
            return jwtDecode<TokenPayload>(this.accessToken);
        }
        return null;
    }
}