import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ITeamListViewModel as ITeamsListViewModel, ITeamListViewModel } from 'src/app/teams/models/team.models';
import { TeamsService } from 'src/app/teams/services/teams.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-team-edit',
  templateUrl: './team-edit.component.html',
  styleUrls: ['./team-edit.component.css']
})
export class TeamEditComponent implements OnInit {

  sortColumn: string = "Name";
  sortOrder: string = "Asc";
  filterColumn: string = "Name";
  title: string;
  form: FormGroup;
  team: ITeamsListViewModel;
  id?: number;

  constructor(private activatedRoute: ActivatedRoute,
    private teamsService: TeamsService,
    private router: Router) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      id: new FormControl(''),
      name: new FormControl(''),
      dateOfFoundation: new FormControl(''),
      ownerName: new FormControl(''),
      championShipPoints: new FormControl('')
    });

    this.loadData();
  }

  loadData() {
    this.id = +this.activatedRoute.snapshot.paramMap.get('id');

    if (this.id) {
      this.teamsService.getTeamById<ITeamsListViewModel>(this.id).subscribe(result => {
        this.team = result;
        this.title = 'Edit: ' + this.team.name;
        this.form.patchValue(this.team);
      }, error => console.log(error));
    }
    else {
      this.title = 'Add new Team member';
    }
  }

  onSubmit() {
    var team = (this.id) ? this.team : <ITeamsListViewModel>{};

    team.name = this.form.get('name').value;
    team.championShipPoints = this.form.get('championShipPoints').value;
    team.dateOfFoundation = this.form.get('dateOfFoundation').value;
    team.ownerName = this.form.get('ownerName').value;

    if (this.id) {
      //update existing team
      this.teamsService.putTeam<ITeamListViewModel>(this.id, team).subscribe(result => {
        this.router.navigate(['/teams']);
      }, error => console.log('error'));
    }
    else {
      //create new team
      this.teamsService.postTeam<ITeamListViewModel>(team).subscribe(result => {
        this.router.navigate(['/teams']);
      }, error => console.log(error));
    }
  }
}
