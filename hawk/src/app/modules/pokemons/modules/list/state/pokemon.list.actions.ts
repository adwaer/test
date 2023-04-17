export class PokemonListFetch {
  static readonly type = '[Pokemon List] Fetch';

  constructor(public limit: number, public offset: number) {
  }
}
