import {CartItem} from "./pokemon.cart.models";

export class PokemonCartAdd {
  static readonly type = '[Pokemon Cart] Add';

  constructor(public item: CartItem) {
  }
}

export class PokemonCartRemove {
  static readonly type = '[Pokemon Cart] Remove';

  constructor(public itemId: number) {
  }
}
