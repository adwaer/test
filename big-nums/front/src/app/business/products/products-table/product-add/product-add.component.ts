import {Component, ChangeDetectionStrategy, Inject} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Disposable, isLoading} from '../../../../shared/helpers';
import {AddProduct, EditProduct} from '../../state/actions/product-crud.actions';
import {Actions, Store} from '@ngxs/store';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {AlertSuccess} from '../../../../core/state/alert.actions';
import {ProductViewModel} from '../../../../shared/api';

@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html',
  styleUrls: ['./product-add.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductAddComponent extends Disposable {

  form!: FormGroup;
  loading$ = isLoading(this.actions$, this.disposed$, [AddProduct]);

  constructor(formBuilder: FormBuilder,
              @Inject(MAT_DIALOG_DATA) data: ProductViewModel,
              private dialogRef: MatDialogRef<any>,
              private actions$: Actions,
              private store: Store) {
    super();

    if (data) {
      const {id, name, price} = data;
      this.form = formBuilder.group({
        id: [id],
        name: [name, [Validators.required, Validators.minLength(2)]],
        price: [price, [Validators.required, Validators.min(1)]]
      });
    } else {
      this.form = formBuilder.group({
        name: ['', [Validators.required, Validators.minLength(2)]],
        price: ['', [Validators.required, Validators.min(1)]]
      });
    }
  }

  public async onSubmit(): Promise<void> {
    if (this.form.invalid) {
      return;
    }

    const {name, price, id} = this.form.value;
    this.store
      .dispatch(id ? new EditProduct(id, name, price) : new AddProduct(name, price))
      .subscribe(() => {
        this.store.dispatch(new AlertSuccess(`The product ${name} has successfully saved!`));
        this.dialogRef.close();
      });
  }

}
