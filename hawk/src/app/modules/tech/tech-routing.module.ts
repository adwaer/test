import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {TechComponent} from "./tech.component";

const routes: Routes = [
  {
    path: '',
    component: TechComponent,
    children: [
      {
        path: '404',
        loadChildren: () => import('./not-found/not-found.module').then(m => m.NotFoundModule)
      },
      {
        path: '500',
        loadChildren: () => import('./srv-error/srv-error.module').then(m => m.SrvErrorModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TechRoutingModule {
}
