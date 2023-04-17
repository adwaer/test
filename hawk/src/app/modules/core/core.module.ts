import {NgModule, Optional, SkipSelf} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {AbsoluteUrlInterceprot} from "./interceptors/absolute-url-interceprot";
import {PokemonApiService} from "./api/pokemon-api.service";
import {environment} from "../../../environments/environment";
import {NgxsModule} from "@ngxs/store";
import {NgxsStoragePluginModule} from "@ngxs/storage-plugin";


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule,
    NgxsModule.forRoot([], {
      developmentMode: !environment.production
    }),
    NgxsStoragePluginModule.forRoot({
      key: 'pokemon_cart'
    })
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AbsoluteUrlInterceprot, multi: true},
    PokemonApiService
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error(
        'CoreModule is already loaded. Import it in the AppModule only'
      );
    }
  }
}
