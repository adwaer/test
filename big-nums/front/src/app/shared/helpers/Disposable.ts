import { Subject } from 'rxjs';
import { Directive, OnDestroy } from '@angular/core';

@Directive()
// tslint:disable-next-line:directive-class-suffix
export abstract class Disposable implements OnDestroy {
  private disposedSubj$ = new Subject();
  protected disposed$ = this.disposedSubj$.asObservable();

  ngOnDestroy(): void {
    this.disposedSubj$.next();
    this.disposedSubj$.complete();
  }
}
