import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CountriesComponent} from './components/countries/countries.component';
import {DashRoutingModule} from './dash-routing.module';
import {MaterialModule} from '../../material/module';
import {ReactiveFormsModule} from '@angular/forms';
import { CompletedComponent } from './components/completed/completed.component';

@NgModule({
  declarations: [CountriesComponent, CompletedComponent],
  imports: [
    CommonModule,
    DashRoutingModule,
    MaterialModule,
    ReactiveFormsModule
  ]
})
export class DashModule {
}
