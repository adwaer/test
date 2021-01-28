import {Component, ChangeDetectionStrategy, HostListener} from '@angular/core';
import {Observable, Subject} from 'rxjs';

@Component({
  selector: 'app-installer',
  templateUrl: './installer.component.html',
  styleUrls: ['./installer.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class InstallerComponent {

  canInstall$ = new Subject<boolean>();
  private deferredPrompt: any;

  promptEvent$: Observable<any>;

  constructor() {
    this.promptEvent$ = this.canInstall$
      .asObservable();
  }

  @HostListener('window:beforeinstallprompt', ['$event'])
  private onBeforeInstallPrompt(e: Event): void {
    e.preventDefault();
    this.deferredPrompt = e;
    this.canInstall$.next(true);
  }

  installPwa(): void {
    this.canInstall$.next(false);
    this.deferredPrompt.prompt();

    this.deferredPrompt.userChoice
      .then((choiceResult: any) => {
        if (choiceResult.outcome === 'accepted') {
          console.log('User accepted the A2HS prompt');
        } else {
          console.log('User dismissed the A2HS prompt');
        }
        this.deferredPrompt = null;
      });
  }

}
