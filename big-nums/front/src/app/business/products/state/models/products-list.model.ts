import {ProductViewModel} from '../../../../shared/api';

export interface IProductListStateModel {
  filters: IProductListStateFilter;
  data: ProductViewModel[] | undefined;
  totalItemCount: number | undefined;
}

export interface IProductListStateFilter {
  search: string | null | undefined;
  orderBy: string | null | undefined;
  isDesc: boolean | undefined;
  page: number | undefined;
  pageSize: number | undefined;
}
