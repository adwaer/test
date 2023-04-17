import {ChangeDetectionStrategy, Component, Input, OnInit} from '@angular/core';
import {PokemonCartAdd, PokemonCartRemove} from "../../../state/pokemon.cart.actions";
import {Select, Store} from "@ngxs/store";
import {map, mergeMap, Observable} from "rxjs";
import {PokemonCartState} from "../../../state/pokemon.cart.state";
import {CartItem} from "../../../state/pokemon.cart.models";

@Component({
  selector: 'app-pokemon-cart-button',
  templateUrl: './pokemon-cart-button.component.html',
  styleUrls: ['./pokemon-cart-button.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PokemonCartButtonComponent implements OnInit {
  @Input() id!: number;
  @Input() name!: string;
  @Input() price!: number;

  @Select(PokemonCartState.data)
  cartItems$!: Observable<CartItem[]>;

  isInCart$!: Observable<boolean>;

  constructor(private store: Store) {
  }

  ngOnInit(): void {
    this.isInCart$ = this.cartItems$.pipe(
      map(cartItems => cartItems
        .findIndex(ca => this.id === ca.id) !== -1)
    );
  }

  addToCart() {
    this.store.dispatch(new PokemonCartAdd({
      id: this.id,
      name: this.name,
      price: this.price
    }))
  }

  removeFromCart() {
    this.store.dispatch(new PokemonCartRemove(this.id))
  }

}
