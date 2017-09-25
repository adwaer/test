import {CommonModule} from '@angular/common';
import {NgModule} from '@angular/core';

import {MainRoutingModule} from './main-routing.module';
import {MainComponent} from './component';
import {FormsModule} from '@angular/forms';
import {LoaderModule} from '../controls/loader/module';
import {ValidationModule} from '../controls/validation/module';
import {SidebarModule} from '../controls/sidebar/module';
import {HeaderModule} from "../controls/header/module";

@NgModule({
    imports: [
        FormsModule,
        CommonModule,
        ValidationModule,
        LoaderModule,
        SidebarModule,
        HeaderModule,
        MainRoutingModule
    ],
    declarations: [MainComponent]
})
export class MainModule {
}
