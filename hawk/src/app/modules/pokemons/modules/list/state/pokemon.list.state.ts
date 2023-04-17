import {Injectable} from "@angular/core";
import {Action, Selector, State, StateContext} from "@ngxs/store";
import {PokemonListItemStateModel, PokemonListStateModel} from "./pokemon.list.models";
import {PokemonListFetch} from "./pokemon.list.actions";
import {finalize, tap} from "rxjs";
import {patch} from "@ngxs/store/operators";
import {NamedAPIResource} from "@core/api/models/common";
import {PokemonApiService} from "@core/api/pokemon-api.service";
import {getRandomInt} from "@core/domain/getRandomInt";

const pokemonUrl = 'https://pokeapi.co/api/v2/pokemon/';
const pokemonUrlLen = pokemonUrl.length;
const mapResponse = (data: NamedAPIResource[]): PokemonListItemStateModel[] => {
  return data.map(item => <PokemonListItemStateModel>({
    id: +item.url.substring(pokemonUrlLen, item.url.length - 1),
    price: getRandomInt(),
    ...item
  }))
}

@State<PokemonListStateModel>({
  name: 'pokemon_list',
  defaults: {
    paging: {
      limit: 10,
      offset: 0
    },
    data: [],
    isLoading: false,
    total: 0
  }
})
@Injectable()
export class PokemonListState {
  @Selector()
  static isLoading(state: PokemonListStateModel) {
    return state.isLoading;
  }

  @Selector()
  static paging(state: PokemonListStateModel) {
    return state.paging;
  }

  @Selector()
  static total(state: PokemonListStateModel) {
    return state.total;
  }

  @Selector()
  static data(state: PokemonListStateModel) {
    return state.data;
  }

  constructor(private api: PokemonApiService) {
  }

  @Action(PokemonListFetch)
  public fetchList(ctx: StateContext<PokemonListStateModel>, { limit, offset }: PokemonListFetch) {
    const state = ctx.getState();

    ctx.setState(patch({isLoading: true}));
    return this.api.listPokemons(offset, limit)
      .pipe(
        tap(result => {
          ctx.setState({
            ...state,
            data: mapResponse(result.results),
            paging: { limit, offset },
            total: result.count,
          })
        }),
        finalize(() => ctx.setState(patch({isLoading: false})))
      )
  }
}
