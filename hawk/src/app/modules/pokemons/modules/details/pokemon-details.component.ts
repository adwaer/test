import {ChangeDetectionStrategy, Component, OnInit, Self} from '@angular/core';
import {Select, Store} from "@ngxs/store";
import {filter, map, Observable, takeUntil} from "rxjs";
import {PokemonDetailsState} from "./state/pokemon.details.state";
import {ActivatedRoute} from "@angular/router";
import {PokemonDetailsFetch} from "./state/pokemon.details.actions";
import {Sprites, spritesToImages} from "./helpers";
import {PokemonDetailsData} from "./state/pokemon.details.models";
import {NgOnDestroy} from "@core/domain/NgOnDestroy";

@Component({
  selector: 'app-pokemon-details',
  templateUrl: './pokemon-details.component.html',
  styleUrls: ['./pokemon-details.component.sass'],
  providers: [NgOnDestroy],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PokemonDetailsComponent implements OnInit {

  @Select(PokemonDetailsState.isLoading)
  isLoading$!: Observable<boolean>;

  @Select(PokemonDetailsState.data)
  data$!: Observable<PokemonDetailsData>;

  images$: Observable<Sprites>;
  avatar$: Observable<string>;

  private id$: Observable<number>;

  constructor(
    private store: Store,
    private route: ActivatedRoute,
    @Self() private onDestroy$: NgOnDestroy) {
    this.id$ = this.route.params.pipe(map(p => p['id']));
    this.images$ = this.data$.pipe(
      filter(data => !!data),
      map(data => spritesToImages(data.sprites))
    )

    this.avatar$ = this.images$.pipe(
      map(images => images.avatars[0])
    )
  }

  ngOnInit(): void {
    this.id$.pipe(
      filter(id => !!id),
      takeUntil(this.onDestroy$)
    ).subscribe(id => {
      this.store.dispatch(new PokemonDetailsFetch(id));
    });
  }

}
