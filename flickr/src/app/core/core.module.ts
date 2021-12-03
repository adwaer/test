import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {NgxsModule} from "@ngxs/store";
import {environment} from "../../environments/environment";
import {PhotosState} from "./state/photos.state";
import {HttpClientModule} from "@angular/common/http";
import {API_KEY} from "./services/flickr.service";


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule,
    NgxsModule.forRoot([PhotosState], {
      developmentMode: !environment.production
    })
  ],
  providers: [
    { provide: API_KEY, useValue: environment.api_key },
  ]
})
export class CoreModule {
}
