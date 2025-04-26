
export class Utils {

    static isNullOrWhitespace(v: unknown): boolean {
        return v == null || (typeof v === 'string' && v.trim().length === 0);
    }

    static hasValue(v: unknown): boolean {
        if (v == null) return false; 
    
        if (typeof v === 'number') return true;
    
        if (typeof v === 'boolean') return true;
    
        if (typeof v === 'string' && v.trim().length > 0) return true;
    
        if (Array.isArray(v) && v.length > 0) return true;
    
        if (typeof v === 'object' && Object.keys(v).length > 0) return true;
    
        return false;
    }
}