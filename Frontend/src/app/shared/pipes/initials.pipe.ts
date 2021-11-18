import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'initials'
})
export class InitialsPipe implements PipeTransform {
  transform(name: string): string {
    return name.split(' ')[0][0] + name.split(' ')[1][0];
  }
}