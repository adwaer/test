export class AddProduct {
  static readonly type = '[Products] Add';

  constructor(public name: string, public price: number) {
  }
}

export class EditProduct {
  static readonly type = '[Products] Edit';

  constructor(public id: number, public name: string, public price: number) {
  }
}

export class DeleteProduct {
  static readonly type = '[Products] Delete';

  constructor(public id: number) {
  }
}
