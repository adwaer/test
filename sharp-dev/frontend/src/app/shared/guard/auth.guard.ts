import {Injectable} from '@angular/core';
import {CanActivate} from '@angular/router';
import {Router} from '@angular/router';
import {UserService} from '../../modules/auth/_services/user.service';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router, private userService: UserService) {
    }

    canActivate() {
        if (this.userService.isAuthenticated()) {
            return true;
        }

        this.router.navigate(['/login']);
        return false;
    }
}
