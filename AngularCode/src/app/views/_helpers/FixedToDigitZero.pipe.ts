import { PipeTransform, Pipe } from '@angular/core';
@Pipe({ name: 'twozero' })
export class FixedToDigitZero implements PipeTransform {
    transform(value: number, args: string[]): any {
        if (!value) {
            return value;
        }
        return value.toFixed(2);
    }
}