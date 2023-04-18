import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PokemonCardComponent} from "./pokemon-card/pokemon-card.component";
import {MatCardModule} from "@angular/material/card";
import {RouterLink} from "@angular/router";
import {MatButtonModule} from "@angular/material/button";
import {PokemonCartButtonComponent} from './pokemon-cart-button/pokemon-cart-button.component';
import { PokemonBuyCardComponent } from './pokemon-buy-card/pokemon-buy-card.component';

@NgModule({
  declarations: [
    PokemonCardComponent,
    PokemonCartButtonComponent,
    PokemonBuyCardComponent
  ],
  exports: [PokemonCardComponent, PokemonCartButtonComponent, PokemonBuyCardComponent],
  imports: [
    CommonModule,
    MatCardModule,
    RouterLink,
    MatButtonModule
  ]
})
export class PokemonSharedModule {
}
