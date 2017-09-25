import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {SignupRoutingModule} from './signup-routing.module';
import {SignupComponent} from './signup.component';
import {FormsModule} from '@angular/forms';
import {ValidationModule} from '../../controls/validation/module';
import {LoaderModule} from '../../controls/loader/module';
import {AuthenticationModule} from '../module';

@NgModule({
    imports: [
        FormsModule,
        CommonModule,
        ValidationModule,
        LoaderModule,
        SignupRoutingModule,
        AuthenticationModule
    ],
    declarations: [SignupComponent]
})
export class SignupModule {
}
