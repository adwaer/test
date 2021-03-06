﻿import {Injectable} from '@angular/core';
import 'rxjs/add/operator/map'
import {Client, LoginViewModel, RegViewModel} from '../../../services/api/swagger';
import {Helpers} from '../../../services/api/apihelpers';

@Injectable()
export class UserService {
    private currentUser: LoginViewModel;

    constructor(private api: Client) {
    }

    public isAuthenticated(): boolean {
        if (!this.currentUser) {
            this.loadFromStore();
        }
        return !!this.currentUser;
    }

    get CurrentUser(): LoginViewModel {
        if (!this.currentUser) {
            this.loadFromStore();
        }
        return this.currentUser;
    }

    private loadFromStore() {
        const userData = JSON.parse(localStorage.getItem('currentUser'));
        this.currentUser = userData && new LoginViewModel(userData)
    }

    public async login(creds: LoginViewModel): Promise<any> {
        const result = await Helpers.withAsync(this.api.login_Get(creds.login, creds.password));

        this.currentUser = creds;
        localStorage.setItem('currentUser', JSON.stringify(creds));

        return result;
    }

    public async reg(creds: RegViewModel): Promise<any> {
        const result = await Helpers.withAsync(this.api.login_Post(creds));
        await this.login(new LoginViewModel({
            login: creds.email,
            password: creds.password
        }));

        return result;
    }

    public logout() {
        localStorage.removeItem('currentUser');
        location.reload();
    }
}
