import {ChangeDetectionStrategy, Component, Input} from '@angular/core';
import {PokemonCartAdd, PokemonCartRemove} from "../../../state/pokemon.cart.actions";
import {Select, Store} from "@ngxs/store";
import {first, map, Observable} from "rxjs";
import {PokemonCartState} from "../../../state/pokemon.cart.state";
import {CartItem} from "../../../state/pokemon.cart.models";
import {getRandomInt} from "@core/domain/getRandomInt";

@Component({
  selector: 'app-pokemon-cart-button',
  templateUrl: './pokemon-cart-button.component.html',
  styleUrls: ['./pokemon-cart-button.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PokemonCartButtonComponent {
  @Input() id!: number;
  @Input() name!: string;

  @Select(PokemonCartState.data)
  cartItems$!: Observable<CartItem[]>;

  @Select(PokemonCartState.cartItem)
  cartItemFn$!: Observable<(id: number) => CartItem | undefined>;

  cartItem$: Observable<CartItem | undefined>;
  price$: Observable<number>;
  private priceRand?: number;

  constructor(private store: Store) {
    this.cartItem$ = this.cartItemFn$.pipe(
      map(fn => fn(this.id))
    );
    this.price$ = this.cartItem$.pipe(
      map(item => this.getPrice(item))
    );
  }

  addToCart(): void {
    this.price$.pipe(
      first()
    ).subscribe(price => {
      this.store.dispatch(new PokemonCartAdd({
        id: this.id,
        name: this.name,
        price: price
      }))
    })
  }

  removeFromCart(): void {
    this.store.dispatch(new PokemonCartRemove(this.id))
  }

  private getPrice(cartItem?: CartItem): number {
    if (cartItem) {
      this.priceRand = cartItem.price;
      return cartItem.price;
    }

    return this.priceRand || (this.priceRand = getRandomInt(100));
  }

}
