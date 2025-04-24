
export class DateUtils {

    public static addDays(date: Date, days: number) {
        const copyDate = new Date(date);
        copyDate.setDate(date.getDate() + days);
        return copyDate;
    }

    public static generateFirstDayOfMonth(month?: number, year?: number): Date {
        const today = new Date();
        month ||= today.getMonth() + 1;
        year ||= today.getFullYear();
        const startDate = new Date(year, month - 1, 1); 
        return startDate;
    }

    public static generateLastDayOfMonth(month?: number, year?: number): Date {
        const today = new Date();
        month ||= today.getMonth() + 1;
        year ||= today.getFullYear();
        const startDate = new Date(year, month, 0); 
        return startDate;
    }
}