import {Inject, Injectable, InjectionToken} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs/internal/Observable";
import {map} from "rxjs/operators";

export const API_KEY = new InjectionToken<string>('API_KEY');
const API_URL = 'https://www.flickr.com/services/rest';

@Injectable({
  providedIn: 'root'
})
export class FlickrService {

  constructor(private http: HttpClient, @Inject(API_KEY) private apiKey: string) {
  }

  search(pattern: string): Observable<IFlickerPhoto[]> {
    const url = `${API_URL}?method=flickr.photos.search&api_key=${this.apiKey}&text=${pattern}&per_page=20&format=json&nojsoncallback=1`;

    // todo: add error handling
    return this.http.get(url)
      .pipe(
        map(data => data as any),
        map(({photos: { photo }}) => {
          return photo as IFlickerPhoto[];
        })
      );
  }
}

export interface IFlickerPhoto {
  id: number;
  owner: string;
  title: string;
  server: string;
  secret: string
}
