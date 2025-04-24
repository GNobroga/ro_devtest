import { Utils } from "./utils";

export class QueryParamsUtils {

    static toQueryString(obj: Record<string, any>): string {
        if (typeof obj !== 'object' || obj === null) {
            throw new Error('Parameter must be a non-null object');
        }

        const params: Record<string, any> = {};

        Object.entries(obj).forEach(([key, value]) => {
            if (!Utils.isNullOrWhitespace(value)) {
                params[key] = value;
            }
        });

        return new URLSearchParams(params).toString();
    }
}