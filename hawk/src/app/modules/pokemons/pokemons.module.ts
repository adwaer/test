import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {PokemonsRoutingModule} from './pokemons-routing.module';
import {PokemonsComponent} from './pokemons.component';
import {NgxsModule} from "@ngxs/store";
import {PokemonCartState} from "./state/pokemon.cart.state";
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatIconModule} from "@angular/material/icon";
import {MatBadgeModule} from "@angular/material/badge";
import {MatButtonModule} from "@angular/material/button";
import {MatTooltipModule} from "@angular/material/tooltip";

@NgModule({
  declarations: [
    PokemonsComponent
  ],
  imports: [
    CommonModule,
    PokemonsRoutingModule,
    NgxsModule.forFeature([PokemonCartState]),
    MatToolbarModule,
    MatIconModule,
    MatBadgeModule,
    MatButtonModule,
    MatTooltipModule
  ]
})
export class PokemonsModule {
}
