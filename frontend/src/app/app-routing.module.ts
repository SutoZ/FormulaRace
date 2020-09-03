import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PilotsComponent } from './pilots/components/pilots.component';
import { TeamsComponent } from './teams/components/teams.component';
import { PilotEditComponent } from './pilots/components/pilot-edit/pilot-edit.component';

const routes: Routes = [
  { path: 'pilots', component: PilotsComponent },
  { path: 'pilots/:id', component: PilotEditComponent },
  { path: 'pilot', component: PilotEditComponent },
  //{ path: 'pilots/pilot', component: PilotEditComponent },
  { path: 'api/teams', component: TeamsComponent },

];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
