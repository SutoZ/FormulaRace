import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { PilotsComponent } from './pilots/components/pilots.component';
import { NavMenuComponent } from './nav-menu/components/nav-menu.component';


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
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
