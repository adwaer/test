import {CommonModule} from '@angular/common';
import {NgModule} from '@angular/core';

import {MainRoutingModule} from './main-routing.module';
import {MainComponent} from './component';
import {FormsModule} from '@angular/forms';
import {LoaderModule} from '../controls/loader/module';
import {ValidationModule} from '../controls/validation/module';
import {SidebarModule} from '../controls/sidebar/module';
import {HeaderModule} from "../controls/header/module";
import {Ng2AutoCompleteModule} from "ng2-auto-complete";

@NgModule({
    imports: [
        FormsModule,
        CommonModule,
        ValidationModule,
        LoaderModule,
        Ng2AutoCompleteModule,
        SidebarModule,
        HeaderModule,
        MainRoutingModule
    ],
    declarations: [MainComponent]
})
export class MainModule {
}
