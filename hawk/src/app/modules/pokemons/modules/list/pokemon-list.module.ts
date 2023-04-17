import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PokemonListRoutingModule } from './pokemon-list-routing.module';
import { PokemonListComponent } from './pokemon-list.component';
import {MatCardModule} from "@angular/material/card";
import {MatPaginatorModule} from "@angular/material/paginator";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import {MatChipsModule} from "@angular/material/chips";
import {MatButtonModule} from "@angular/material/button";
import {PokemonSharedModule} from "../shared/pokemon-shared.module";
import {NgxsModule} from "@ngxs/store";
import {PokemonListState} from "./state/pokemon.list.state";


@NgModule({
  declarations: [
    PokemonListComponent
  ],
  imports: [
    CommonModule,
    PokemonListRoutingModule,
    MatCardModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatChipsModule,
    MatButtonModule,
    PokemonSharedModule,
    NgxsModule.forFeature([PokemonListState])
  ]
})
export class PokemonListModule { }
