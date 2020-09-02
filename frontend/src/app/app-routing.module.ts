import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PilotsComponent } from './pilots/components/pilots.component';
import { TeamsComponent } from './teams/components/teams.component';
import { PilotEditComponent } from './pilots/components/pilot-edit/pilot-edit.component';

const routes: Routes = [
  { path: 'pilots', component: PilotsComponent },
  { path: 'api/teams', component: TeamsComponent },
  { path: 'pilots/:id', component: PilotEditComponent }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
