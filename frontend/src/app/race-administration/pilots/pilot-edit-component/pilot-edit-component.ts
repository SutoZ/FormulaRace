import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatError, MatFormField, MatLabel } from '@angular/material/form-field';
import { MatOption, MatSelect } from '@angular/material/select';
import { ActivatedRoute } from '@angular/router';
import { IPilotsListViewModel } from '../../models/pilot.models';
import { HttpParams } from '@angular/common/http';
import { TeamsService } from 'src/app/race-administration/services/teams.service';
import { IPagedList } from 'src/app/PagedList';
import { ITeamListViewModel } from 'src/app/race-administration/models/team.models';
import { PilotsService } from '../../services/pilotservice';

@Component({
  selector: 'app-pilot-edit-component',
  imports: [MatFormField, MatLabel, MatError, MatSelect, MatOption, ReactiveFormsModule],
  templateUrl: './pilot-edit-component.html',
  styleUrl: './pilot-edit-component.css',
})
export class PilotEditComponent implements OnInit {
  form: FormGroup;
  title: string;
  id: number | undefined;
  pilot: IPilotsListViewModel | undefined;
  teams: ITeamListViewModel[] = [];

  constructor(
    private readonly fb: FormBuilder,
    private readonly activatedRoute: ActivatedRoute,
    private readonly pilotService: PilotsService,
    private readonly teamsService: TeamsService
  ) {
    this.form = new FormGroup({});
    this.title = 'Create new Pilot';
  }

  ngOnInit() {
    this.form = this.fb.group({
      name: new FormControl('', { updateOn: 'blur' }),
      number: [new FormControl(''), Validators.required],
      code: [new FormControl(''), Validators.required],
      nationality: [new FormControl(''), Validators.required],
      teamId: [new FormControl(''), Validators.required],
    });

    this.loadTeams();

    const idParam = this.activatedRoute.snapshot.paramMap.get('id');
    this.id = idParam ? +idParam : undefined;
    if (this.id) {
      this.title = 'Loading...';
    } else {
      this.title = 'Create new Pilot';
    }
    if (this.id) {
      this.pilotService.getPilotById(this.id).subscribe({
        next: (pilot) => {
          this.pilot = pilot;
          this.title = `Edit: ${pilot.name}`;
          this.form.patchValue(pilot);
        },
        error: (err) => console.error(err),
      });
    }
  }

  loadTeams() {
    const params = new HttpParams()
      .set('pageIndex', '0')
      .set('pageSize', '100')
      .set('sortColumn', 'Name')
      .set('sortOrder', 'asc')
      .set('filterColumn', '')
      .set('filterQuery', '');

    this.teamsService.getTeams<IPagedList<ITeamListViewModel>>(params).subscribe({
      next: (result) => (this.teams = result.data),
      error: (err) => console.error(err),
    });
  }

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const data: IPilotsListViewModel = {
      ...this.pilot,
      ...this.form.value,
    };

    if (this.id) {
      this.pilotService.updatePilot(this.id, data).subscribe({
        next: () => {
          console.log('Pilot updated successfully');
        },
        error: (err) => console.error(err),
      });
    } else {
      this.pilotService.createPilot(data).subscribe({
        next: () => {
          console.log('Pilot created successfully');
        },
        error: (err) => console.error(err),
      });
    }
  }
}
