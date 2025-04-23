export interface CreateUser {
    userName: string;
    name: string;
    email: string;
    password: string;
    passwordConfirmation: string;
}

export interface CreateUserResult {
    id: string;
    userName: string;
    name: string;
    email: string;
}

