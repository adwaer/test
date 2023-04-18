import {Injectable} from "@angular/core";
import {Action, Selector, State, StateContext} from "@ngxs/store";
import {CartItem, PokemonCartStateModel} from "./pokemon.cart.models";
import {PokemonCartAdd, PokemonCartRemove} from "./pokemon.cart.actions";

@State<PokemonCartStateModel>({
  name: 'pokemon_cart',
  defaults: {
    data: []
  }
})
@Injectable()
export class PokemonCartState {
  @Selector()
  static data(state: PokemonCartStateModel) {
    return state.data;
  }

  @Selector()
  static count(state: PokemonCartStateModel) {
    return state.data.length;
  }

  @Selector()
  static cartItem(state: PokemonCartStateModel): (id: number) => CartItem | undefined {
    return (id: number) => {
      const items = state.data;
      return items.find(item => item.id === id);
    }
  }

  @Action(PokemonCartAdd)
  public add(ctx: StateContext<PokemonCartStateModel>, {item}: PokemonCartAdd) {
    const {data} = ctx.getState();
    ctx.setState({
      data: [
        ...data,
        item
      ]
    })
  }

  @Action(PokemonCartRemove)
  public remove(ctx: StateContext<PokemonCartStateModel>, {itemId}: PokemonCartRemove) {
    const {data} = ctx.getState();

    const result = data.filter(item => item.id !== itemId);

    ctx.setState({
      data: result
    });
  }
}
