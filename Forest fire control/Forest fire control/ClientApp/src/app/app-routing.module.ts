import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CounterComponent } from './counter/counter.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './services/auth.guard';
import { HomeComponent } from './home/home.component';
import { CreateUserComponent } from './create-user/create-user.component';
import { CreateObservationComponent } from './create-observation/create-observation.component';
import { ObservationSiteComponent } from './observation-site/observation-site.component';
import { CreateApplicationComponent } from './application/create-application/create-application.component';
import { ApplicationTableComponent } from './application/application-table/application-table.component';
import { RoleGuard } from './services/role.guard';
import { ApplicationComponent } from './application/application.component';

const routes: Routes = [
  { path: 'counter', component: CounterComponent, canActivate: [AuthGuard] },
  { path: 'applications', component: ApplicationTableComponent, canActivate: [RoleGuard] },
  { path: 'login', component: LoginComponent },
  { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'create-user', component: CreateUserComponent },
  { path: 'create-observation', component: CreateObservationComponent, canActivate: [RoleGuard] },
  { path: 'observation-site', component: ObservationSiteComponent, canActivate: [AuthGuard] },
  { path: 'create-application', component: CreateApplicationComponent, canActivate: [AuthGuard] },
  { path: 'application', component: ApplicationComponent, canActivate: [RoleGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }