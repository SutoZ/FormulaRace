import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatOptionModule } from '@angular/material/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ITeamListViewModel } from '../../models/team.models';
import { PilotsService } from '../../services/pilotservice';
import { TeamsService } from '../../services/teams.service';

@Component({
  selector: 'app-pilot-create-component',
  imports: [
    MatCardModule,
    MatFormFieldModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatOptionModule,
  ],
  templateUrl: './pilot-create-component.html',
  styleUrl: './pilot-create-component.css',
})
export class PilotCreateComponent implements OnInit {
  createPilotForm: FormGroup;
  teams: ITeamListViewModel[] = [];

  constructor(
    private readonly fb: FormBuilder,
    private readonly pilotsService: PilotsService,
    private readonly teamsService: TeamsService,
    private readonly router: Router,
    private readonly snackBar: MatSnackBar
  ) {
    this.createPilotForm = this.fb.group({
      name: ['', Validators.required],
      number: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      code: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(3)]],
      nationality: ['', Validators.required],
      teamId: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.loadTeams();
  }

  loadTeams(): void {
    // Assuming getTeams can fetch all teams without pagination for the dropdown
    this.teamsService.getTeams().subscribe({
      next: (teamList) => {
        this.teams = teamList;
      },
      error: () => {
        this.snackBar.open('Error loading teams', 'Close', { duration: 3000 });
      },
    });
  }

  onSubmit(): void {
    if (this.createPilotForm.invalid) {
      // Mark all fields as touched to display validation errors
      this.createPilotForm.markAllAsTouched();
      return;
    }

    this.pilotsService.createPilot(this.createPilotForm.value).subscribe({
      next: () => {
        this.snackBar.open('Pilot created successfully', 'Close', {
          duration: 2000,
        });
        this.router.navigate(['/pilots']);
      },
      error: (err) => {
        console.error(err);
        this.snackBar.open('Error creating pilot', 'Close', {
          duration: 3000,
        });
      },
    });
  }
}
