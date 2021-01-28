import {Component, ChangeDetectionStrategy} from '@angular/core';
import {Disposable, isLoading} from '../../../shared/helpers';
import {Actions, ofActionDispatched, Store} from '@ngxs/store';
import {FetchProducts, FilterProducts, SearchProducts} from '../state/actions/products-list.actions';
import {ProductsTableDatasource} from './products-table-datasource';
import {MatDialog} from '@angular/material/dialog';
import {ProductAddComponent} from './product-add/product-add.component';
import {debounceTime, distinctUntilChanged, map, takeUntil} from 'rxjs/operators';
import {DeleteProduct} from '../state/actions/product-crud.actions';
import {ProductViewModel} from '../../../shared/api';

@Component({
  selector: 'app-products-table',
  templateUrl: './products-table.component.html',
  styleUrls: ['./products-table.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductsTableComponent extends Disposable {

  displayedColumns: string[] = ['name', 'price', 'actions'];
  loading$ = isLoading(this.actions$, this.disposed$, [FetchProducts]);
  dataSource: ProductsTableDatasource;

  constructor(private actions$: Actions,
              private store: Store,
              private dialog: MatDialog) {
    super();
    this.dataSource = new ProductsTableDatasource(store);

    actions$
      .pipe(
        ofActionDispatched(SearchProducts),
        map((action: SearchProducts) => action.search),
        debounceTime(500),
        distinctUntilChanged(),
        takeUntil(this.disposed$)
      )
      .subscribe(search => {
        store.dispatch(new FilterProducts(search));
      });
  }

  add(): void {
    this.dialog.open(ProductAddComponent);
  }

  edit(product: ProductViewModel): void {
    this.dialog.open(ProductAddComponent, {
      data: product
    });
  }

  delete(id: number, name: string): void {
    if (confirm(`Are you sure want to delete ${name}?`)) {
      this.store.dispatch(new DeleteProduct(id));
    }
  }

  applyFilter(event: KeyboardEvent): void {
    const input = event.target as HTMLInputElement;
    const filterValue = input.value;

    console.log(filterValue);

    this.store.dispatch(new SearchProducts(filterValue));
  }
}
