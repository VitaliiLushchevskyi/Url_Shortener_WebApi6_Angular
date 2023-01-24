import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './views/nav-menu/nav-menu.component';
import { HomeComponent } from './pages/home/home.component';
import router from './router';
import { NgxPaginationModule } from 'ngx-pagination';
import { InputUrlsViewComponent } from './views/input-urls-view/input-urls-view.component';
import { UrlShortener } from './services/url.service';
import { BrowserModule } from '@angular/platform-browser';
import TableUrlsViewComponent from './pages/table-urls/table-urls-view.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { RegistrationPageComponent } from './pages/registration-page/registration-page.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    InputUrlsViewComponent,
    TableUrlsViewComponent,
    LoginPageComponent,
    RegistrationPageComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    router,
    NgxPaginationModule,
  ],
  providers: [UrlShortener],
  bootstrap: [AppComponent],
})
export class AppModule {}
