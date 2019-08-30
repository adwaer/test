import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable, of} from 'rxjs';
import {Router} from '@angular/router';
import {Client, CreateAccountRequest} from '../_api/identity';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private authCredsSubj$: BehaviorSubject<string>;
  public authCreds$: Observable<string>;

  constructor(private router: Router, private client: Client) {
    const currentUserStore = localStorage.getItem('currentUser');
    const userFromStore = currentUserStore ? JSON.parse(currentUserStore) : null;

    this.authCredsSubj$ = new BehaviorSubject<string>(userFromStore);
    this.authCreds$ = this.authCredsSubj$.asObservable();

    this.authCreds$.subscribe(user => {
      if (user) {
        localStorage.setItem('currentUser', JSON.stringify(user));
      } else {
        localStorage.removeItem('currentUser');
      }
    });
  }

  login(data) {
    const {email, password, confirmPass} = data;
    return this.client.createUser(new CreateAccountRequest({
      email,
      password,
      confirmPassword: confirmPass
    }))
      .pipe(map(response => {
        this.authCredsSubj$.next(response.token);
      }));

  }

  async logout(): Promise<any> {
    this.authCredsSubj$.next(null);
    await this.router.navigate(['/auth'], {queryParams: {returnUrl: this.router.routerState.snapshot.url}});
  }
}
