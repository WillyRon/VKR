import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './services/auth.guard';
import { HomeComponent } from './home/home.component';
import { CreateUserComponent } from './create-user/create-user.component';
import { CreateObservationComponent } from './observation-site/create-observation/create-observation.component';
import { ObservationSiteComponent } from './observation-site/observation-site.component';
import { CreateApplicationComponent } from './application/create-application/create-application.component';
import { ApplicationTableComponent } from './application/application-table/application-table.component';
import { RoleGuard } from './services/role.guard';
import { ApplicationComponent } from './application/application.component';
import { ObservationTableComponent } from './observation-site/observation-table/observation-table.component';
import { EditObservationComponent } from './observation-site/edit-observation/edit-observation.component';

const routes: Routes = [
  { path: 'applications', component: ApplicationTableComponent, canActivate: [RoleGuard] },
  { path: 'login', component: LoginComponent },
  { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'create-user', component: CreateUserComponent },
  { path: 'create-observation', component: CreateObservationComponent, canActivate: [RoleGuard] },
  { path: 'edit-observation', component: EditObservationComponent, canActivate: [RoleGuard] },
  { path: 'observation-site', component: ObservationSiteComponent, canActivate: [AuthGuard] },
  { path: 'create-application', component: CreateApplicationComponent, canActivate: [AuthGuard] },
  { path: 'observations', component: ObservationTableComponent, canActivate: [AuthGuard] },
  { path: 'application', component: ApplicationComponent, canActivate: [RoleGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }