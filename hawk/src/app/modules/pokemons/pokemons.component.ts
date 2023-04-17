import { ChangeDetectionStrategy, Component } from '@angular/core';
import {Select} from "@ngxs/store";
import {PokemonCartState} from "./state/pokemon.cart.state";
import {Observable} from "rxjs";

@Component({
  selector: 'app-pokemons',
  templateUrl: './pokemons.component.html',
  styleUrls: ['./pokemons.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PokemonsComponent {
  @Select(PokemonCartState.count)
  cartCount$!: Observable<number>;
}
