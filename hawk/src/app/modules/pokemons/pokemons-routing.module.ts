import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {PokemonsComponent} from "./pokemons.component";

const routes: Routes = [{
  path: '',
  component: PokemonsComponent,
  children: [
    {
      path: '',
      loadChildren: () => import('./modules/list/pokemon-list.module').then(m => m.PokemonListModule)
    },
    {
      path: 'cart',
      loadChildren: () => import('./modules/cart/pokemon-cart.module').then(m => m.PokemonCartModule)
    },
    {
      path: ':id',
      loadChildren: () => import('./modules/details/pokemon-details.module').then(m => m.PokemonDetailsModule)
    }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PokemonsRoutingModule {
}
