import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { FlexLayoutModule } from '@angular/flex-layout';
import { AngularMaterialModule } from './angular-material.module'
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { AddTransactionDialogComponent } from './dialogs/add-transaction/add-transaction-dialog.component';

import { authInterceptorProviders } from './helpers/auth.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    AddTransactionDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    AngularMaterialModule,
    HttpClientModule,
    FlexLayoutModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [authInterceptorProviders],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
