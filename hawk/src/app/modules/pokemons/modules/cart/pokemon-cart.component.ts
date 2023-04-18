import {ChangeDetectionStrategy, Component} from '@angular/core';
import {Select} from "@ngxs/store";
import {PokemonCartState} from "../../state/pokemon.cart.state";
import {map, Observable} from "rxjs";
import {CartItem} from "../../state/pokemon.cart.models";

@Component({
  selector: 'app-pokemon-cart',
  templateUrl: './pokemon-cart.component.html',
  styleUrls: ['./pokemon-cart.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PokemonCartComponent {
  @Select(PokemonCartState.data)
  items$!: Observable<CartItem[]>;

  totals$: Observable<{ count: number, sum: number }>;

  constructor() {
    this.totals$ = this.items$.pipe(
      map(items => {
        const count = items.length;
        const sum = items
          .map(item => item.price)
          .reduce((a, b) => a + b, 0)
        return {count, sum}
      })
    )
  }

  trackById(_: number, item: CartItem): number {
    return item.id;
  }
}
