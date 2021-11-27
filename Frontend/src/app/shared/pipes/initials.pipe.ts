import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'initials'
})
export class InitialsPipe implements PipeTransform {
    public transform(name: string): string {
        let splitName = name.split(' ');
        return splitName[0][0] + splitName[splitName.length - 1][0];
    }
}
