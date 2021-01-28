import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {BusinessRoutingModule} from './business-routing.module';
import {SharedModule} from '../shared/shared.module';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BusinessRoutingModule,
    SharedModule
  ]
})
export class BusinessModule {
}
