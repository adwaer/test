import {Pokemon} from "@core/api/models/pokemon";

export interface PokemonDetailsStateModel {
  pokemonId?: number,
  data?: PokemonDetailsData;
  isLoading: boolean;
}

export interface PokemonDetailsData extends Pokemon {
  price: number;
}
