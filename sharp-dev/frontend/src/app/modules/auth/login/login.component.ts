import {Component, ViewChild} from '@angular/core';
import {Router} from '@angular/router';
import {routerTransition} from '../../../router.animations';
import {NgForm} from '@angular/forms';
import {UserService} from '../_services/user.service';
import {Helpers} from '../../../services/api/apihelpers';
import {LoginViewModel} from '../../../services/api/swagger';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    animations: [routerTransition()]
})
export class LoginComponent {

    public model: LoginViewModel = new LoginViewModel();
    public isLoading = false;
    @ViewChild('form') form: NgForm;

    constructor(public router: Router, private userService: UserService) {
    }

    async onSubmit() {
        if (this.form.invalid) {
            return;
        }

        this.isLoading = true;
        try {
            await this.userService.login(this.model);
            this.router.navigate(['/main']);
        }
        finally {
            this.isLoading = false;
        }
    }
}
