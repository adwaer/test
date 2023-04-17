import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {PokemonCartComponent} from "./pokemon-cart.component";

const routes: Routes = [
  {
    path: '',
    component: PokemonCartComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PokemonCartRoutingModule { }
