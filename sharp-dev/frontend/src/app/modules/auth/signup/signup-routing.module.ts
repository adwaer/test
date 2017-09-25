import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SignupComponent } from './signup.component';
import {AuthNotGuard} from '../../../shared/guard/auth.not.guard';

const routes: Routes = [
    { path: '', component: SignupComponent, canActivate: [AuthNotGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SignupRoutingModule { }
