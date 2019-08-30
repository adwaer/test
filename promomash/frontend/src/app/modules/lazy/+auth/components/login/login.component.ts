import {Component, ViewChild} from '@angular/core';
import {FormBuilder, FormGroupDirective, Validators} from '@angular/forms';
import {AuthenticationService} from '../../../../../_services/authentication.service';
import {BehaviorSubject} from 'rxjs';
import {RxwebValidators} from '@rxweb/reactive-form-validators';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent {

  loginForm = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.pattern('^(?=.*[A-Z])(?=.*\\d).+$')]],
    confirmPass: ['', [Validators.required, RxwebValidators.compare({fieldName: 'password'})]],
    termsAcc: ['']
  });
  isLoading$ = new BehaviorSubject<boolean>(false);
  serverError$ = new BehaviorSubject<string>(null);

  @ViewChild('formDir', {static: false}) formDir: FormGroupDirective;

  constructor(private formBuilder: FormBuilder,
              private authService: AuthenticationService,
              private router: Router,
              private route: ActivatedRoute) {
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      return;
    }

    this.isLoading$.next(true);
    this.authService.login(this.loginForm.value)
      .subscribe(() => {
          const returnUrl = this.route.snapshot.queryParams.returnUrl || '/';
          this.router.navigate([returnUrl]);
          this.isLoading$.next(false);
        }
        , error => {
          this.isLoading$.next(false);
          this.serverError$.next(error.message);
        });
  }
}
