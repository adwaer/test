import {Action, Selector, State, StateContext} from "@ngxs/store";
import {Injectable} from "@angular/core";
import {PokemonDetailsStateModel} from "./pokemon.details.models";
import {PokemonApiService} from "@core/api/pokemon-api.service";
import {PokemonListStateModel} from "../../list/state/pokemon.list.models";
import {PokemonDetailsFetch} from "./pokemon.details.actions";
import {patch} from "@ngxs/store/operators";
import {finalize, tap} from "rxjs";

@State<PokemonDetailsStateModel>({
  name: 'pokemon_details',
  defaults: {
    pokemonId: undefined,
    data: undefined,
    isLoading: false,
  }
})
@Injectable()
export class PokemonDetailsState {
  @Selector()
  static data(state: PokemonDetailsStateModel) {
    return state.data;
  }

  @Selector()
  static isLoading(state: PokemonListStateModel) {
    return state.isLoading;
  }

  constructor(private api: PokemonApiService) {
  }

  @Action(PokemonDetailsFetch)
  public fetchList(ctx: StateContext<PokemonDetailsStateModel>, {id}: PokemonDetailsFetch) {
    const state = ctx.getState();

    ctx.setState(patch({isLoading: true}));
    return this.api.getPokemonById(id)
      .pipe(
        tap(result => {
          ctx.setState({
            ...state,
            pokemonId: id,
            data: {
              ...result
            }
          })
        }),
        finalize(() => ctx.setState(patch({isLoading: false})))
      )
  }
}
