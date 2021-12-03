import {Injectable} from "@angular/core";
import {Action, Selector, State, StateContext} from "@ngxs/store";
import {IPhotosStateModel} from "./photos-state.model";
import {SetPhotoSearch} from "./photos.actions";
import {FlickrService, IFlickerPhoto} from "../services/flickr.service";
import {Observable} from "rxjs/internal/Observable";
import {tap} from "rxjs/operators";
import {of} from "rxjs/internal/observable/of";

@State<IPhotosStateModel>({
  name: 'photos',
  defaults: {
    search: undefined,
    images: []
  }
})

@Injectable()
export class PhotosState {
  @Selector()
  static images(state: IPhotosStateModel): IFlickerPhoto[] {
    return state.images;
  }

  constructor(private api: FlickrService) {
  }

  @Action(SetPhotoSearch)
  setSearch(ctx: StateContext<IPhotosStateModel>, {payload}: SetPhotoSearch): Observable<IFlickerPhoto[]> {
    const state = ctx.getState();

    if (state.search === payload) {
      return of(state.images);
    }

    return this.api.search(payload)
      .pipe(
        tap(photos => {
          ctx.setState({
            ...state,
            search: payload,
            images: photos
          })
        })
      )
  }

}
