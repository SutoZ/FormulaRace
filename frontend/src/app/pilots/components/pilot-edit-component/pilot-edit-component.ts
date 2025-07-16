import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PilotsService } from '../../services/pilots.service';
import { TeamsService } from 'src/app/teams/services/teams.service';
import { IPilotsListViewModel } from '../../models/pilot.models';
import { ITeamListViewModel } from 'src/app/teams/models/team.models';
import { PagedList } from 'src/app/PagedList';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-pilot-edit',
  templateUrl: './pilot-edit-component.html',
  styleUrls: ['./pilot-edit-component.css']
})
export class PilotEditComponent implements OnInit {
  form!: FormGroup;
  pilot: IPilotsListViewModel | null = null;
  teams: ITeamListViewModel[] = [];
  id?: number;
  title = 'Create new Pilot';

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly pilotService: PilotsService,
    private readonly teamsService: TeamsService,
    private readonly router: Router
  ) { }

  ngOnInit() {
    this.form = new FormGroup({
      name: new FormControl('', {
        validators: [Validators.required],
        updateOn: 'blur'
      }),
      number: new FormControl('', Validators.required),
      code: new FormControl('', [Validators.required, Validators.maxLength(3)]),
      nationality: new FormControl('', Validators.required),
      teamId: new FormControl('', Validators.required)
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
        next: pilot => {
          this.pilot = pilot;
          this.title = `Edit: ${pilot.name}`;
          this.form.patchValue(pilot);
        },
        error: err => console.error(err)
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

    this.teamsService.getTeams<PagedList<ITeamListViewModel>>(params).subscribe({
      next: result => (this.teams = result.data),
      error: err => console.error(err)
    });
  }

  onSubmit() {
    if (this.form.invalid)
      return;

    const pilotData: IPilotsListViewModel = {
      ...this.pilot,
      ...this.form.value
    };

    if (this.id) {
      this.pilotService.putPilot(this.id, pilotData).subscribe({
        next: () => { this.router.navigate(['/pilots']); },
        error: err => console.error(err)
      });
    } else {
      this.pilotService.postPilot(pilotData).subscribe({
        next: () => { this.router.navigate(['/pilots']); },
        error: err => console.error(err)
      });
    }
  }

  // checkNameExists(): AsyncValidatorFn {
  //   return (control: AbstractControl): Observable<{ [key: string]: any } | null> => {
  //     if (!control.value) return of(null);
  //     const id = this.id ?? 0;
  //     return this.pilotService.checkNameExists({ id, name: control.value }).pipe(
  //       debounceTime(300),
  //       map(exists => (exists ? { nameExists: true } : null)),
  //       first()
  //     );
  //   };
  // }
}