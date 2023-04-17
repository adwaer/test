export class PokemonDetailsFetch {
  static readonly type = '[Pokemon Details] Fetch';

  constructor(public id: number) {
  }
}
