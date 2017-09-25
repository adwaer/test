import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {HeaderComponent} from './header.component';
import {RouterModule} from '@angular/router';
import {ApiModule} from '../../../services/api/module';
import {LoaderModule} from '../loader/module';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {Ng2AutoCompleteModule} from 'ng2-auto-complete';
import {AuthenticationModule} from '../../auth/module';
import {ValidationModule} from '../validation/module';
import {MovementModule} from '../movement/module';

@NgModule({
    imports: [
        FormsModule,
        CommonModule,
        RouterModule,
        AuthenticationModule,
        NgbModule.forRoot(),
        Ng2AutoCompleteModule,
        ValidationModule,
        ApiModule,
        LoaderModule,
        MovementModule
    ],
    declarations: [HeaderComponent],
    exports: [HeaderComponent]
})

export class HeaderModule {
}
