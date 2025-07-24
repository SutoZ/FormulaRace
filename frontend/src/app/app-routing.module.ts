import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PilotListComponent } from './race-administration/pilots/pilot-list-component/pilot-list-component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full',
  },
  {
    path: 'pilots',
    component: PilotListComponent,
  },
  // {
  //   path: 'pilots/:id',
  //   component: PilotEditComponent
  // },
  // {
  //   path: 'pilot',
  //   component: PilotEditComponent
  // },
  // {
  //   path: 'teams',
  //   component: TeamsComponent
  // },
  // {
  //   path: 'teams/:id',
  //   component: TeamEditComponent
  // }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
