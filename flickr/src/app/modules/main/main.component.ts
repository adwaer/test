import {Component, OnInit, ChangeDetectionStrategy} from '@angular/core';
import {Actions, Select, Store} from "@ngxs/store";
import {Observable} from "rxjs/internal/Observable";
import {PhotosState} from "../../core/state/photos.state";
import {isLoading} from "../../core/helpers/loading";
import {Disposable} from "../../core/helpers/Disposable";
import {SetPhotoSearch} from "../../core/state/photos.actions";
import {IFlickerPhoto} from "../../core/services/flickr.service";
import {BehaviorSubject} from "rxjs";
import {debounceTime, distinctUntilChanged, filter, map, takeUntil} from "rxjs/operators";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MainComponent extends Disposable {
  @Select(PhotosState.images) photos$!: Observable<IFlickerPhoto[]>;
  isLoading$ = isLoading(this.actions, this.disposed$, [SetPhotoSearch]);
  private search$ = new BehaviorSubject<string>('popular');

  constructor(private store: Store,
              private actions: Actions) {
    super();

    this.search$
      .pipe(
        map(val => (val + '').trim()),
        filter(val => val.length > 0),
        debounceTime(500),
        distinctUntilChanged(),
        takeUntil(this.disposed$)
      )
      .subscribe(search => {
        this.store.dispatch(new SetPhotoSearch(search));
      });
  }

  setSearch(ev: Event): void {
    const target = ev.target as HTMLTextAreaElement;
    this.search$.next(target.value);
  }

}
