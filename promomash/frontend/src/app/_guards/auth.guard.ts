import {Injectable} from '@angular/core';
import {Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {AuthenticationService} from '../_services/authentication.service';
import {map} from 'rxjs/operators';

@Injectable({providedIn: 'root'})
export class AuthGuard implements CanActivate {

  constructor(private router: Router, private authService: AuthenticationService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.authService.authCreds$.pipe(
      map(user => {
        if (!user) {
          this.router.navigate(['/auth'], { queryParams: { returnUrl: state.url }});
          return false;
        }
        return true;
      })
    );
  }
}
