import { Observable, OperatorFunction, startWith, Subject, switchMap } from "rxjs";

export function initialize<T>(factory: () => void): OperatorFunction<T, T> {
    return source$ => {
        factory();
        return source$;
    };
}