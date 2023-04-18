import {NamedAPIResource} from "@core/api/models/common";

export interface PokemonListStateModel {
  paging: Paging
  total: number;
  isLoading: boolean;
  data: PokemonListItemStateModel[];
}

export interface PokemonListItemStateModel extends NamedAPIResource {
  id: number;
}

export interface Paging {
  limit: number;
  offset: number;
}
