
export interface TokenStorage {
    accessToken: string;
    refreshToken: string;
};

export interface LoginCredentials {
    username: string;
    password: string;
}

export interface TokenInfo {
    accessToken: string;
    refreshToken: string;
    issuedAt: Date;
    expirationDate: Date;
    roles: string[];
}

export interface User {
    id: string;
    userName: string;
    name: string;
    email: string;
}