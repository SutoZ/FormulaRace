import { BrowserModule } from '@angular/platform-browser';
import { NgModule, InjectionToken } from '@angular/core';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { PilotsComponent } from './pilots/components/pilots.component';
import { NavMenuComponent } from './nav-menu/components/nav-menu.component';


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
