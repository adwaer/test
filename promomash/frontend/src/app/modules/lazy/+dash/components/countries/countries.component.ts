import {Component} from '@angular/core';
import {Observable, of, Subject} from 'rxjs';
import {Client, CountryViewModel, ProvinceViewModel} from '../../../../../_api/identity';
import {flatMap} from 'rxjs/operators';
import {FormBuilder, Validators} from '@angular/forms';
import {NavigationExtras, Router} from '@angular/router';

@Component({
  selector: 'app-countries',
  templateUrl: './countries.component.html',
  styleUrls: ['./countries.component.sass']
})
export class CountriesComponent {

  countries$: Observable<CountryViewModel[]>;
  provinces$: Observable<ProvinceViewModel[]>;
  filterChange$ = new Subject();

  countryForm = this.formBuilder.group({
    country: ['', [Validators.required]],
    province: ['', [Validators.required]]
  });

  constructor(client: Client, private formBuilder: FormBuilder, private router: Router) {
    this.countries$ = client.getAll();
    this.provinces$ = this.filterChange$
      .pipe(flatMap(() => this.countryForm.value.country ? client.get(this.countryForm.value.country.id) : of([])));
  }

  public onSubmit() {
    if (this.countryForm.invalid) {
      return;
    }

    this.router.navigate(['completed']);
  }
}
