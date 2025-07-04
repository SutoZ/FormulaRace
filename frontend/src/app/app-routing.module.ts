import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PilotsComponent } from './pilots/components/pilot-list/pilots.component';
import { TeamsComponent } from './teams/components/team-list/teams.component';
import { TeamEditComponent } from './teams/components/team-edit/team-edit/team-edit.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full'
  },
  {
    path: 'pilots',
    component: PilotsComponent,
  },
  // {
  //   path: 'pilots/:id',
  //   component: PilotEditComponent
  // },
  // {
  //   path: 'pilot',
  //   component: PilotEditComponent
  // },
  {
    path: 'teams',
    component: TeamsComponent
  },
  {
    path: 'teams/:id',
    component: TeamEditComponent
  }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
