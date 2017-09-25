import {NgModule} from '@angular/core';
import {UserService} from './_services/user.service';
import {FormsModule} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {ApiModule} from '../../services/api/module';

@NgModule({
    imports: [
        FormsModule,
        CommonModule,
        ApiModule
    ]
})

export class AuthenticationModule {
    static forRoot() {
        return {
            ngModule: AuthenticationModule,
            providers: [UserService]
        };
    }
}
