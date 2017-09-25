import {Component, ViewChild} from '@angular/core';
import {routerTransition} from '../../../router.animations';
import {NgForm} from '@angular/forms';
import {RegViewModel} from '../../../services/api/swagger';
import {Router} from '@angular/router';
import {UserService} from '../_services/user.service';
import {NotifyService} from '../../inceptors/errors/notifyService';

@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.scss'],
    animations: [routerTransition()]
})
export class SignupComponent {
    public model: RegViewModel = new RegViewModel();
    public repeatPassword = '';
    public isLoading = false;
    @ViewChild('form') form: NgForm;


    constructor(public router: Router, private userService: UserService) {
    }

    async onSubmit() {
        if (this.form.invalid) {
            return;
        }
        if (this.model.password !== this.repeatPassword) {
            NotifyService.messagePublished$.emit({message: 'Passwords are not the same', status: 400});
            return;
        }

        this.isLoading = true;
        try {
            await this.userService.reg(this.model);
            this.router.navigate(['/main']);
        }
        finally {
            this.isLoading = false;
        }
    }
}
