import {Observable} from 'rxjs/Observable';

export class Helpers {
  public static withAsync(obs: Observable<any | null>): Promise<any> {
    return obs.toPromise();
  }
}
