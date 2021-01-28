import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatTableModule} from '@angular/material/table';
import {API_BASE_URL, ApiClient} from './api';
import {environment} from '../../environments/environment';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {LoaderComponent} from './loader/loader.component';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatDialogModule} from '@angular/material/dialog';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {ErrorInterceptor} from '@shared/interceptors/error.interceptor';


@NgModule({
  declarations: [LoaderComponent],
  imports: [
    CommonModule,
    MatProgressSpinnerModule,
    HttpClientModule
  ],
  exports: [
    MatTableModule,
    MatSnackBarModule,
    MatPaginatorModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    LoaderComponent,
    HttpClientModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: API_BASE_URL, useValue: environment.api},
    ApiClient
  ]
})
export class SharedModule {
}
