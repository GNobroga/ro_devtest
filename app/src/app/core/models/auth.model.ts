
export interface TokenStorage {
    accessToken: string;
    refreshToken: string;
};

export interface LoginCredentials {
    username: string;
    password: string;
}


export interface TokenPayload {
    iss: number;
    unique_name: string;
    email: string;
    sub: string;
    role: string;
    exp: number;
    aud: string;
}

export interface TokenInfo {
    accessToken: string;
    refreshToken: string;
    issuedAt: Date;
    expirationDate: Date;
    roles: string[];
}
