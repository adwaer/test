import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./modules/main/main.module').then(m => m.MainModule),
  },
  {
    path: 'pokemons',
    loadChildren: () => import('./modules/pokemons/pokemons.module').then(m => m.PokemonsModule),
  },
  {
    path: 'tech',
    loadChildren: () => import('./modules/tech/tech.module').then(m => m.TechModule),
  },
  {
    path: '**',
    redirectTo: 'tech/404'
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
