import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login.component';
import {AuthNotGuard} from '../../../shared/guard/auth.not.guard';

const routes: Routes = [
    { path: '', component: LoginComponent, canActivate: [AuthNotGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LoginRoutingModule { }
