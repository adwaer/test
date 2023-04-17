import {ChangeDetectionStrategy, Component, OnInit} from '@angular/core';
import {Select, Store} from "@ngxs/store";
import {filter, map, mergeMap, Observable, takeUntil} from "rxjs";
import {PokemonDetailsState} from "./state/pokemon.details.state";
import {ActivatedRoute} from "@angular/router";
import {PokemonDetailsFetch} from "./state/pokemon.details.actions";
import {Disposable} from "@core/domain/disposable";
import {Sprites, spritesToImages} from "./helpers";
import {PokemonCartState} from "../../state/pokemon.cart.state";
import {PokemonListItemStateModel} from "../list/state/pokemon.list.models";
import {PokemonDetailsData} from "./state/pokemon.details.models";

@Component({
  selector: 'app-pokemon-details',
  templateUrl: './pokemon-details.component.html',
  styleUrls: ['./pokemon-details.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PokemonDetailsComponent extends Disposable implements OnInit {

  @Select(PokemonDetailsState.isLoading)
  isLoading$!: Observable<boolean>;

  @Select(PokemonDetailsState.data)
  data$!: Observable<PokemonDetailsData>;

  @Select(PokemonCartState.data)
  cartItems$!: Observable<PokemonListItemStateModel[]>;

  id$: Observable<number>;
  images$: Observable<Sprites>;
  price$!: Observable<number>;

  constructor(private store: Store, private route: ActivatedRoute) {
    super();
    this.id$ = this.route.params.pipe(map(p => p['id']));
    this.images$ = this.data$.pipe(
      filter(data => !!data),
      map(data => spritesToImages(data.sprites))
    )

    this.price$ = this.data$.pipe(
      mergeMap(item => this.cartItems$.pipe(
        map(cartItems => {
          const foundItem = cartItems
            .find(ca => item.id === ca.id);

          return foundItem ? foundItem.price: item.price;
        })
      ))
    );
  }

  ngOnInit(): void {
    this.id$.pipe(
      filter(id => !!id),
      takeUntil(this.disposed$)
    ).subscribe(id => {
      this.store.dispatch(new PokemonDetailsFetch(id));
    });
  }

}
