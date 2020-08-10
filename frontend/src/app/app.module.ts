import { BrowserModule } from '@angular/platform-browser';
import { NgModule, InjectionToken } from '@angular/core';
import { AppComponent } from './app.component';
import { PilotsComponent } from './components/pilots/pilots.component';
import { RouterModule } from '@angular/router';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HttpClientModule } from '@angular/common/http';
import { environment } from 'src/environments/environment';


const BASE_URL = new InjectionToken<string>('API_BASE_URL');

@NgModule({
  declarations: [
    AppComponent,
    PilotsComponent,
    NavMenuComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot([
      {
        path: 'api/pilots', component: PilotsComponent
      }])
  ],
  providers: [
    { provide: BASE_URL, useValue: environment.BASE_URL }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
