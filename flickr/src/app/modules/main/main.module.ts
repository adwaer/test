import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {MainRoutingModule} from './main-routing.module';
import {MainComponent} from './main.component';
import {MaterialModule} from "../shared/material/material.module";
import {PhotoCardComponent} from './photo-card/photo-card.component';
import {ControlsModule} from "../shared/controls/controls.module";


@NgModule({
  declarations: [
    MainComponent,
    PhotoCardComponent
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    MaterialModule,
    ControlsModule
  ]
})
export class MainModule {
}
