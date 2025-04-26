export interface CreateOrUpdateUser {
    userName: string;
    name: string;
    email: string;
    password: string;
    passwordConfirmation: string;
}

export interface CreateOrUpdateUserResult {
    id: string;
    userName: string;
    name: string;
    email: string;
    roles: string[];
}

export interface DeleteUserResult {
    userId: string;
    deleted: boolean;
}

export interface User {
    id: string;
    userName: string;
    name: string;
    email: string;
    creationOn: Date;
    modifiedOn: Date;
}