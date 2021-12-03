import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AppCustomPreloader} from "./app-custom.preloader";

const routes: Routes = [{
  path: '',
  pathMatch: 'full',
  loadChildren: () => import('./modules/main/main.module').then(m => m.MainModule),
  data: {preload: true},
}, {
  path: '**',
  redirectTo: '',
},
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {preloadingStrategy: AppCustomPreloader})],
  exports: [RouterModule],
  providers: [AppCustomPreloader]
})
export class AppRoutingModule {
}
