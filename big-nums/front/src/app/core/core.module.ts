import {NgModule, Optional, SkipSelf} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CoreComponent} from './core.component';
import {InstallerComponent} from './components/installer/installer.component';
import {SharedModule} from '@shared/shared.module';
import {AlertState} from './state/alert.state';
import {NgxsModule} from '@ngxs/store';
import {environment} from '../../environments/environment';
import {NgxsReduxDevtoolsPluginModule} from '@ngxs/devtools-plugin';
import {NotificationsComponent} from './components/notifications/notifications.component';
import {LayoutComponent} from './components/layout/layout.component';


@NgModule({
  declarations: [CoreComponent, InstallerComponent, NotificationsComponent, LayoutComponent],
  exports: [
    CoreComponent,
    LayoutComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    NgxsModule.forRoot([AlertState], {
      developmentMode: !environment.production
    }),
    NgxsReduxDevtoolsPluginModule.forRoot({
      disabled: environment.production,
    })
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
