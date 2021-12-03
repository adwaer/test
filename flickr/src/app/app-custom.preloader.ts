import { Injectable } from '@angular/core';
import { PreloadingStrategy, Route } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';

@Injectable()
export class AppCustomPreloader implements PreloadingStrategy {
  preload(route: Route, load: () => Observable<never>): Observable<never> {
    return route.data && route.data.preload ? load() : of();
  }
}
