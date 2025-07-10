import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NavMenuComponent } from './nav-menu/components/nav-menu.component';
import { AngularMaterialModule } from './angular-material/angular-material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { HomeComponent } from './home/home.component';
// import { TeamEditComponent } from './teams/components/team-edit/team-edit/team-edit.component';
// import { TeamsComponent } from './teams/components/team-list/teams.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatOptionModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule } from '@angular/forms';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';


@NgModule({ declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
    ],
    bootstrap: [AppComponent], imports: [BrowserModule,
        AppRoutingModule,
        AngularMaterialModule,
        BrowserAnimationsModule,
        MatFormFieldModule,
        MatInputModule,
        MatPaginatorModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatDialogModule,
        MatOptionModule,
        ReactiveFormsModule,
        MatSelectModule], providers: [provideHttpClient(withInterceptorsFromDi())] })
export class AppModule { }
