export class FetchProducts {
  static readonly type = '[Products] Fetch';

  constructor() {
  }
}

export class ChangeProductsPage {
  static readonly type = '[Products] Change Page';

  constructor(public page: number, public size: number) {
  }
}

export class ChangeProductsOrder {
  static readonly type = '[Products] Change order';

  constructor(public field: string, public desc: boolean) {
  }
}

export class SearchProducts {
  static readonly type = '[Products] Search';

  constructor(public search: string) {
  }
}

export class FilterProducts {
  static readonly type = '[Products] Filter';

  constructor(public search: string) {
  }
}
