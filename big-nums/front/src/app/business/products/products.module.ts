import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {ProductsRoutingModule} from './products-routing.module';
import {ProductsTableComponent} from './products-table/products-table.component';
import {SharedModule} from '../../shared/shared.module';
import {NgxsModule} from '@ngxs/store';
import {ProductsListState} from './state/state/products-list.state';
import {ProductAddComponent} from './products-table/product-add/product-add.component';
import {ReactiveFormsModule} from '@angular/forms';


@NgModule({
  declarations: [ProductsTableComponent, ProductAddComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SharedModule,
    NgxsModule.forFeature([ProductsListState]),
    ProductsRoutingModule
  ]
})
export class ProductsModule {
}
