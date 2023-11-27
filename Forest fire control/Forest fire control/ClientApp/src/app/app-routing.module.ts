import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CounterComponent } from './counter/counter.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './services/auth.guard';
import { HomeComponent } from './home/home.component';
import { CreateUserComponent } from './create-user/create-user.component';
import { CreateObservationComponent } from './create-observation/create-observation.component';

const routes: Routes = [
  { path: 'counter', component: CounterComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'create-user', component: CreateUserComponent },
  { path: 'create-observation', component: CreateObservationComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }