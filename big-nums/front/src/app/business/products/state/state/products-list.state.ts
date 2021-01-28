import {Action, Selector, State, StateContext} from '@ngxs/store';
import {Injectable} from '@angular/core';
import {IProductListStateFilter, IProductListStateModel} from '../models/products-list.model';
import {ChangeProductsOrder, ChangeProductsPage, FetchProducts, FilterProducts} from '../actions/products-list.actions';
import {ApiClient, ProductEditRequestModel, ProductSearchResponse, ProductViewModel} from '../../../../shared/api';
import {Observable} from 'rxjs';
import {debounceTime, distinctUntilChanged, tap} from 'rxjs/operators';
import {AddProduct, DeleteProduct, EditProduct} from '../actions/product-crud.actions';

@State<IProductListStateModel>({
  name: 'products',
  defaults: {
    data: undefined,
    totalItemCount: undefined,
    filters: {
      pageSize: 10,
      page: 1,
      search: '',
      isDesc: false,
      orderBy: undefined
    }
  }
})

@Injectable()
export class ProductsListState {

  @Selector()
  static data(state: IProductListStateModel): ProductViewModel[] | undefined {
    const {data} = state;
    return data;
  }

  @Selector()
  static totalCount(state: IProductListStateModel): number | undefined {
    const {totalItemCount} = state;
    return totalItemCount;
  }

  @Selector()
  static filters(state: IProductListStateModel): IProductListStateFilter {
    const {filters} = state;
    return filters;
  }

  constructor(private api: ApiClient) {
  }

  @Action(FetchProducts)
  fetchProducts(ctx: StateContext<IProductListStateModel>): Observable<ProductSearchResponse> {
    const {filters: {isDesc, orderBy, page, pageSize, search}} = ctx.getState();
    return this.api.productsGetAll(search, orderBy, isDesc, page, pageSize)
      .pipe(
        debounceTime(500),
        distinctUntilChanged(),
        tap(response => {
          ctx.setState({
            ...ctx.getState(),
            data: response.data,
            totalItemCount: response.count
          });
        })
      );
  }

  @Action(ChangeProductsPage)
  changeProductsPage(ctx: StateContext<IProductListStateModel>, {page, size}: ChangeProductsPage): void {
    const {filters} = ctx.getState();

    ctx.patchState({
      filters: {
        ...filters,
        page,
        pageSize: size
      }
    });

    setTimeout(() => {
      ctx.dispatch(new FetchProducts());
    }, 0);
  }

  @Action(ChangeProductsOrder)
  changeProductsOrder(ctx: StateContext<IProductListStateModel>, {field, desc}: ChangeProductsOrder): void {
    const {filters} = ctx.getState();

    ctx.patchState({
      filters: {
        ...filters,
        orderBy: field,
        isDesc: desc
      }
    });

    setTimeout(() => {
      ctx.dispatch(new FetchProducts());
    }, 0);
  }

  @Action(FilterProducts)
  filterProducts(ctx: StateContext<IProductListStateModel>, {search}: FilterProducts): void {
    const {filters} = ctx.getState();

    ctx.patchState({
      filters: {
        ...filters,
        search
      }
    });

    setTimeout(() => {
      ctx.dispatch(new FetchProducts());
    }, 0);
  }

  @Action(AddProduct)
  addProduct(ctx: StateContext<IProductListStateModel>, {name, price}: AddProduct): Observable<void> {
    return this.api
      .productsPost(new ProductEditRequestModel({
        name,
        price
      }))
      .pipe(
        tap(() => {
          setTimeout(() => {
            ctx.dispatch(new FetchProducts());
          }, 0);
        })
      );
  }

  @Action(EditProduct)
  editProduct(ctx: StateContext<IProductListStateModel>, {name, price, id}: EditProduct): Observable<void> {
    return this.api
      .productsPut(id, new ProductEditRequestModel({
        name,
        price
      }))
      .pipe(
        tap(() => {
          setTimeout(() => {
            ctx.dispatch(new FetchProducts());
          }, 0);
        })
      );
  }

  @Action(DeleteProduct)
  deleteProduct(ctx: StateContext<IProductListStateModel>, {id}: DeleteProduct): Observable<void> {
    return this.api
      .productsDelete(id)
      .pipe(
        tap(() => {
          setTimeout(() => {
            ctx.dispatch(new FetchProducts());
          }, 0);
        })
      );
  }
}
