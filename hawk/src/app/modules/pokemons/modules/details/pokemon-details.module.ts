import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {PokemonDetailsRoutingModule} from './pokemon-details-routing.module';
import {PokemonDetailsComponent} from './pokemon-details.component';
import {NgxsModule} from "@ngxs/store";
import {PokemonDetailsState} from "./state/pokemon.details.state";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import {MatCardModule} from "@angular/material/card";
import {MatButtonModule} from "@angular/material/button";
import {PokemonSharedModule} from "../shared/pokemon-shared.module";


@NgModule({
  declarations: [
    PokemonDetailsComponent
  ],
  imports: [
    CommonModule,
    PokemonDetailsRoutingModule,
    NgxsModule.forFeature([PokemonDetailsState]),
    MatProgressBarModule,
    MatCardModule,
    MatButtonModule,
    PokemonSharedModule
  ]
})
export class PokemonDetailsModule {
}
