import {CommonModule} from '@angular/common';
import {NgModule} from '@angular/core';

import {LoginRoutingModule} from './login-routing.module';
import {LoginComponent} from './login.component';
import {AuthenticationModule} from '../module';
import {FormsModule} from '@angular/forms';
import {ValidationModule} from '../../controls/validation/module';
import {LoaderModule} from '../../controls/loader/module';

@NgModule({
    imports: [
        FormsModule,
        CommonModule,
        ValidationModule,
        LoaderModule,
        LoginRoutingModule,
        AuthenticationModule
    ],
    declarations: [LoginComponent]
})
export class LoginModule {
}
