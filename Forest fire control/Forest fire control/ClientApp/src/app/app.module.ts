import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { MatAutocompleteModule, MatCheckboxModule, MatFormFieldModule, MatInputModule } from '@angular/material';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './login/login.component';
import { AuthService } from './services/auth.service';
import { AuthGuard } from './services/auth.guard';
import { CreateUserComponent } from './create-user/create-user.component';
import { ObservationService } from './services/observation.service';
import { CreateObservationComponent } from './create-observation/create-observation.component';
import { UserService } from './services/user.service';
import { ObservationSiteComponent } from './observation-site/observation-site.component';
import { VgBufferingModule, VgControlsModule, VgCoreModule, VgOverlayPlayModule } from 'ngx-videogular';
import { CreateApplicationComponent } from './application/create-application/create-application.component';
import { ApplicationTableComponent } from './application/application-table/application-table.component';
import { RoleGuard } from './services/role.guard';
import { ApplicationComponent } from './application/application.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    CounterComponent,
    CreateUserComponent,
    CreateObservationComponent,
    ObservationSiteComponent,
    CreateApplicationComponent,
    ApplicationTableComponent,
    ApplicationComponent,
  ],

  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatAutocompleteModule,
    MatCheckboxModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    VgCoreModule,
    VgControlsModule,
    VgOverlayPlayModule,
    VgBufferingModule,
  ],
  providers: [
    AuthService,
    AuthGuard,
    RoleGuard,
    ObservationService,
    UserService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
