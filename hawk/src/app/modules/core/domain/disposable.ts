import {Directive, OnDestroy} from "@angular/core";
import {Subject} from "rxjs";

@Directive()
export abstract class Disposable implements OnDestroy {
  private readonly disposedSubj$ = new Subject<void>();
  protected disposed$ = this.disposedSubj$.asObservable();

  ngOnDestroy(): void {
    this.disposedSubj$.next();
    this.disposedSubj$.complete();
  }
}
