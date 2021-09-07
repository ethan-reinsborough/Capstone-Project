import { Pipe, PipeTransform } from '@angular/core';
import { Order } from './models/order';

@Pipe({
  name: 'searchfilter'
})
export class SearchfilterPipe implements PipeTransform {

  transform(Orders: Order[], searchValue: string): Order[] {
    if(!Orders || !searchValue){
      return Orders;
    }

    return Orders.filter(order => order.poNumberFormatted.includes(searchValue))
  }

}