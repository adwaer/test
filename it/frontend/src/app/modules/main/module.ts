import {CommonModule} from '@angular/common';
import {NgModule} from '@angular/core';

import {MainRoutingModule} from './main-routing.module';
import {MainComponent} from './component';
import {FormsModule} from '@angular/forms';
import {LoaderModule} from '../controls/loader/module';
import {ValidationModule} from '../controls/validation/module';

@NgModule({
    imports: [
        FormsModule,
        CommonModule,
        ValidationModule,
        LoaderModule,
        MainRoutingModule
    ],
    declarations: [MainComponent]
})
export class MainModule {
}
