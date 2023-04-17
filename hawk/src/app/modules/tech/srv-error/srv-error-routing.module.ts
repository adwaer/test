import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {SrvErrorComponent} from "./srv-error.component";

const routes: Routes = [
  {
    path: '',
    component: SrvErrorComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SrvErrorRoutingModule {
}
