import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SrvErrorRoutingModule } from './srv-error-routing.module';
import { SrvErrorComponent } from './srv-error.component';


@NgModule({
  declarations: [
    SrvErrorComponent
  ],
  imports: [
    CommonModule,
    SrvErrorRoutingModule
  ]
})
export class SrvErrorModule { }
