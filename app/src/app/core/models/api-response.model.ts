export interface ApiResponse<T> {
    statusCode: number;
    messages?: string[];
    data?: T;
}