export interface CartItem {
  id: number;
  name: string;
  price: number;
}

export interface PokemonCartStateModel {
  data: CartItem[];
}
