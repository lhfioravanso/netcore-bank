import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './helpers/auth.guard';

import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'inicio', component: HomeComponent, canActivate: [ AuthGuard ] },
  { path: '**', redirectTo: 'inicio', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
