import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PokemonCartRoutingModule } from './pokemon-cart-routing.module';
import { PokemonCartComponent } from './pokemon-cart.component';
import {PokemonSharedModule} from "../shared/pokemon-shared.module";


@NgModule({
  declarations: [
    PokemonCartComponent
  ],
    imports: [
        CommonModule,
        PokemonCartRoutingModule,
        PokemonSharedModule
    ]
})
export class PokemonCartModule { }
