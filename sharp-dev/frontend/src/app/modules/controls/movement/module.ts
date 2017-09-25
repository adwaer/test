import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {ApiModule} from '../../../services/api/module';
import {LoaderModule} from '../loader/module';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {Ng2AutoCompleteModule} from 'ng2-auto-complete';
import {AuthenticationModule} from '../../auth/module';
import {ValidationModule} from '../validation/module';
import {MovementDlgComponent} from './dlg.component';
import {ResponseHandlingModule} from '../../inceptors/errors/module';

@NgModule({
    imports: [
        FormsModule,
        ReactiveFormsModule,
        CommonModule,
        RouterModule,
        AuthenticationModule,
        NgbModule.forRoot(),
        Ng2AutoCompleteModule,
        ValidationModule,
        ApiModule,
        LoaderModule,
        ResponseHandlingModule
    ],
    declarations: [MovementDlgComponent],
    entryComponents: [MovementDlgComponent]
})

export class MovementModule {
}
