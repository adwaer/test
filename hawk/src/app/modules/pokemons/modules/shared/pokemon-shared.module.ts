import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PokemonCardComponent} from "./pokemon-card/pokemon-card.component";
import {MatCardModule} from "@angular/material/card";
import {RouterLink} from "@angular/router";
import {MatButtonModule} from "@angular/material/button";
import {PokemonCartButtonComponent} from './pokemon-cart-button/pokemon-cart-button.component';

@NgModule({
  declarations: [
    PokemonCardComponent,
    PokemonCartButtonComponent
  ],
  exports: [PokemonCardComponent, PokemonCartButtonComponent],
  imports: [
    CommonModule,
    MatCardModule,
    RouterLink,
    MatButtonModule
  ]
})
export class PokemonSharedModule {
}
