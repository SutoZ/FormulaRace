import { Component, OnInit } from '@angular/core';
import { TeamsService } from '../services/teams.service';
import { ITeamViewModel } from '../team';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css']
})
export class TeamsComponent implements OnInit {

  public teams: ITeamViewModel[];
  public displayedColumns: string[] = ['Id', 'Name', 'DateOfFoundation', 'OwnerName', 'ChampionShipPoints'];

  constructor(private teamsSerivce: TeamsService) { }

  ngOnInit(): void {
    this.getTeams();
  }

  getTeams(): void {
    this.teamsSerivce.getTeams().subscribe(res => {
      this.teams = res;
    }, error => console.log(error))
  }
}
