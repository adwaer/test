import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {CountriesComponent} from './components/countries/countries.component';
import {CompletedComponent} from './components/completed/completed.component';

const routes: Routes = [
  {
    path: '',
    component: CountriesComponent,
  },
  {
    path: 'completed',
    component: CompletedComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashRoutingModule {
}
