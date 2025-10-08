import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { IPilotsListViewModel } from '../../models/pilot.models';
import { HttpParams } from '@angular/common/http';
import { TeamsService } from 'src/app/race-administration/services/teams.service';
import { ITeamListViewModel } from 'src/app/race-administration/models/team.models';
import { PilotsService } from '../../services/pilotservice';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-pilot-edit-component',
  imports: [
    CommonModule,
    RouterModule,
    MatFormFieldModule,
    MatSelectModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    ReactiveFormsModule,
  ],
  templateUrl: './pilot-edit-component.html',
  styleUrl: './pilot-edit-component.css',
  standalone: true,
})
export class PilotEditComponent implements OnInit {
  form: FormGroup;
  title: string = '';
  id?: number;
  pilot?: IPilotsListViewModel;
  teams: ITeamListViewModel[] = [];

  constructor(
    private readonly fb: FormBuilder,
    private readonly activatedRoute: ActivatedRoute,
    private readonly pilotService: PilotsService,
    private readonly teamsService: TeamsService,
    private readonly snackBar: MatSnackBar
  ) {
    this.form = new FormGroup({});
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

    this.teamsService.getTeams(params).subscribe({
      next: (result) => (this.teams = result.data),
      error: (error) => {
        console.error('Error loading teams:', error);
        let errorMessage = 'Error loading teams';

        if (error.status === 409) {
          errorMessage = 'Conflict error loading teams - check backend configuration';
          console.error(
            '409 Conflict: This may indicate a backend business rule violation or resource conflict'
          );
        } else if (error.status === 404) {
          errorMessage = 'Teams endpoint not found';
        } else if (error.status === 500) {
          errorMessage = 'Server error loading teams';
        } else if (error.status === 0) {
          errorMessage = 'Network error - check if backend is running';
        }

        this.snackBar.open(errorMessage, 'Close', { duration: 5000 });
      },
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
