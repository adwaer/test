import {ChangeDetectionStrategy, Component, OnInit} from '@angular/core';
import {
  Observable,
} from "rxjs";
import {PageEvent} from "@angular/material/paginator";
import {Select, Store} from "@ngxs/store";
import {PokemonListState} from "./state/pokemon.list.state";
import {Paging, PokemonListItemStateModel} from "./state/pokemon.list.models";
import {PokemonListFetch} from "./state/pokemon.list.actions";

@Component({
  selector: 'app-pokemon-list',
  templateUrl: './pokemon-list.component.html',
  styleUrls: ['./pokemon-list.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PokemonListComponent implements OnInit {
  @Select(PokemonListState.data)
  items$!: Observable<PokemonListItemStateModel[]>;

  @Select(PokemonListState.paging)
  public paging$!: Observable<Paging>;

  @Select(PokemonListState.total)
  total$!: Observable<number>;

  @Select(PokemonListState.isLoading)
  isLoading$!: Observable<boolean>;

  constructor(private store: Store) {
  }

  ngOnInit(): void {
    this.store.dispatch(new PokemonListFetch(10, 0));
  }

  setPage(pageModel: PageEvent): void {
    const {pageSize, pageIndex} = pageModel;

    this.store.dispatch(new PokemonListFetch(pageSize, pageIndex * pageSize));
  }

  trackById(_: number, item: PokemonListItemStateModel): number {
    return item.id;
  }
}
