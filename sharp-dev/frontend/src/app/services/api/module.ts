import {CommonModule} from '@angular/common';
import {NgModule} from '@angular/core';
import {Client} from './swagger'
import {HttpWrapperModule} from '../http-wrapper/module';

@NgModule({
    imports: [
        CommonModule,
        HttpWrapperModule
    ],
    providers: [Client]
})
export class ApiModule {
}
