import {CollectionViewer, DataSource} from '@angular/cdk/collections';
import {ProductViewModel} from '../../../shared/api';
import {BehaviorSubject, Observable} from 'rxjs';
import {PageEvent} from '@angular/material/paginator';
import {Select, Store} from '@ngxs/store';
import {ProductsListState} from '../state/state/products-list.state';
import {ChangeProductsOrder, ChangeProductsPage, FetchProducts} from '../state/actions/products-list.actions';
import {IProductListStateFilter} from '../state/models/products-list.model';

export class ProductsTableDatasource extends DataSource<ProductViewModel> {

  @Select(ProductsListState.data)
  private data$!: Observable<ProductViewModel[]>;

  @Select(ProductsListState.data)
  totalCount$!: Observable<number>;

  @Select(ProductsListState.filters)
  filters$!: Observable<IProductListStateFilter>;

  private connected$ = new BehaviorSubject<boolean>(false);

  constructor(private store: Store) {
    super();
  }

  connect(collectionViewer: CollectionViewer): Observable<ProductViewModel[] | ReadonlyArray<ProductViewModel>> {
    this.connected$.next(true);
    this.fetch();

    return this.data$;
  }

  disconnect(collectionViewer: CollectionViewer): void {
    this.connected$.next(false);
  }

  fetch(): void {
    this.store.dispatch(new FetchProducts());
  }

  onPageChange(ev: PageEvent): void {
    this.store.dispatch(new ChangeProductsPage(ev.pageIndex + 1, ev.pageSize));
  }

  setOrder(column: string, desc: boolean): void {
    this.store.dispatch(new ChangeProductsOrder(column, desc));
  }

}
