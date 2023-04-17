import {ChangeDetectionStrategy, Component} from '@angular/core';
import {Select} from "@ngxs/store";
import {PokemonCartState} from "../../state/pokemon.cart.state";
import {Observable} from "rxjs";
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
}
