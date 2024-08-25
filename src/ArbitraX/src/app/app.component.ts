import { Component, signal, OnInit, LOCALE_ID } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ToolbarComponent } from './shared/toolbar/toolbar.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { SecurityService } from './core/services/security.service';
import {
  FormBuilder,
  FormControl,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { LoginInputModel } from './core/InputModels/loginIinputModel';
import { CommonModule, registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { MatToolbarModule } from '@angular/material/toolbar';

registerLocaleData(localePt, 'pt-BR');

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    ToolbarComponent,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    MatButtonModule,
    MatDividerModule,
    MatTooltipModule,
    MatExpansionModule,
    MatSnackBarModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatToolbarModule,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [{ provide: LOCALE_ID, useValue: 'pt-BR' }],
})
export class AppComponent implements OnInit {
  constructor(
    private securityService: SecurityService,
    private snackBar: MatSnackBar,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.checkLogIn();
  }

  readonly panelOpenState = signal(false);

  userIsLoggedIn: boolean = false;
  protected userName: string;
  protected adm: boolean;

  protected loggingIn: boolean = false;

  loginControl = this.formBuilder.group({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
  });

  private checkLogIn() {
    var token = this.securityService.getToken();

    if (token != null) {
      var userInfo = this.securityService.getDecodedAccessToken(token);

      if (userInfo == null) {
        this.userIsLoggedIn = false;
        return;
      }

      const firstSpaceIndex = userInfo.name.indexOf(' ');
      this.userName = userInfo.name.substring(0, firstSpaceIndex);
      this.userIsLoggedIn = true;

      this.adm = userInfo.adm != undefined ? true : false;
    } else {
      this.logOut();
    }

    this.userIsLoggedIn = token != null;
  }

  protected login() {
    const login: LoginInputModel = {
      email: this.loginControl.get('email')!.value!.toString(),
      password: this.loginControl.get('password')!.value!.toString(),
    };

    this.loggingIn = true;

    this.securityService.login(login).subscribe(
      (token) => {
        console.log(token);
        localStorage.setItem('arbitraxUser', JSON.stringify(token));

        this.loginControl.get('email')!.setValue('');
        this.loginControl.get('password')!.setValue('');

        this.userIsLoggedIn = true;
        this.loggingIn = false;

        window.location.reload();
      },
      (error) => {
        if (error.status == 404) {
          this.openSnackBar('Usuário não encontrado');
        }
        if (error.status == 401) {
          this.openSnackBar('Senha inválida');
        }

        this.userIsLoggedIn = false;
        this.loggingIn = false;
      }
    );
  }

  protected logOut() {
    localStorage.removeItem('currentUser');

    window.location.reload();
  }

  public openSnackBar(message: string) {
    this.snackBar.open(message, 'OK', {
      duration: 3000,
    });
  }
}
