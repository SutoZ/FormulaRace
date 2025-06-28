import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NavMenuComponent } from './nav-menu/components/nav-menu.component';
import { AngularMaterialModule } from './angular-material/angular-material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { PilotEditComponent } from './pilots/components/pilot-edit/pilot-edit.component';
import { HomeComponent } from './home/home.component';
import { TeamEditComponent } from './teams/components/team-edit/team-edit/team-edit.component';
import { PilotsComponent } from './pilots/components/pilot-list/pilots.component';
import { TeamsComponent } from './teams/components/team-list/teams.component';
import { MatLegacyOptionModule as MatOptionModule } from '@angular/material/legacy-core';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';


@NgModule({
  declarations: [
    AppComponent,
    PilotsComponent,
    NavMenuComponent,
    TeamsComponent,
    PilotEditComponent,
    HomeComponent,
    TeamEditComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    AngularMaterialModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatInputModule,
    MatOptionModule,
    ReactiveFormsModule,
    MatSelectModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
