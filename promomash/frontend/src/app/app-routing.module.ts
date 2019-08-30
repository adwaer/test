import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AuthGuard} from './_guards/auth.guard';


const routes: Routes = [
  {
    path: 'auth',
    loadChildren: './modules/lazy/+auth/auth.module#AuthModule'
  }, {
    path: '',
    loadChildren: './modules/lazy/+dash/dash.module#DashModule',
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
