import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { NavMenuComponent } from './nav-menu/components/nav-menu.component';
import { AngularMaterialModule } from './angular-material/angular-material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { PilotEditComponent } from './pilots/components/pilot-edit/pilot-edit.component';
import { HomeComponent } from './home/home.component';
import { TeamEditComponent } from './teams/components/team-edit/team-edit/team-edit.component';
import { PilotsComponent } from './pilots/components/pilot-list/pilots.component';
import { TeamsComponent } from './teams/components/team-list/teams.component';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';


@NgModule({
  declarations: [
    AppComponent,
    PilotsComponent,
    NavMenuComponent,
    TeamsComponent,
    PilotEditComponent,
    HomeComponent,
    TeamEditComponent
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
