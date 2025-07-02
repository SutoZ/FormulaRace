// import { Component, OnInit } from '@angular/core';
// import { UntypedFormGroup, UntypedFormControl, Validators, AsyncValidatorFn, AbstractControl } from '@angular/forms';
// import { IPilotsListViewModel } from '../../models/pilot.models';
// import { ActivatedRoute, Router } from '@angular/router';
// import { PilotsService } from '../../services/pilots.service';
// import { ITeamListViewModel } from 'src/app/teams/models/team.models';
// import { TeamsService } from 'src/app/teams/services/teams.service';
// import { HttpParams } from '@angular/common/http';
// import { PagedList } from 'src/app/PagedList';
// import { Observable } from 'rxjs';
// import { map } from 'rxjs/operators';

// @Component({
//   selector: 'app-pilot-edit',
//   templateUrl: './pilot-edit.component.html',
//   styleUrls: ['./pilot-edit.component.css'],
//   standalone: false
// })
// export class PilotEditComponent implements OnInit {
//   sortColumn: string = "Name";
//   sortOrder: string = "Asc";
//   filterColumn: string = "Name";
//   title: string;
//   form: UntypedFormGroup;
//   pilot: IPilotsListViewModel;
//   teams: ITeamListViewModel[];
//   id?: number;
//   defaultIndex: number = 0;
//   defaultPageSize: number = 20;

//   constructor(
//     private activatedRoute: ActivatedRoute,
//     private pilotService: PilotsService,
//     private teamsService: TeamsService,
//     private router: Router
//   ) { }

//   ngOnInit(): void {
//     this.form = new UntypedFormGroup({
//       name: new UntypedFormControl('', Validators.required),
//       number: new UntypedFormControl('', Validators.required),
//       code: new UntypedFormControl('', [Validators.required, Validators.maxLength(3)]),
//       nationality: new UntypedFormControl('', Validators.required),
//       teamId: new UntypedFormControl('', Validators.required)

//     }, null, this.CheckNameExists());
//     this.loadData();
//   }

//   loadData() {
//     this.loadTeams();

//     this.id = +this.activatedRoute.snapshot.paramMap.get('id');
//     if (this.id) {
//       this.pilotService.getPilotById(this.id).subscribe(res => {
//         this.pilot = res;
//         this.title = 'Edit: ' + this.pilot.name;

//         //update form with value of current pilot
//         this.form.patchValue(this.pilot);
//       },
//         error => console.log(error));
//     }
//     else {
//       this.title = 'Create new Pilot!';
//     }
//   }

//   loadTeams() {
//     var params = new HttpParams()
//       .set("pageIndex", this.defaultIndex.toString())
//       .set("pageSize", this.defaultPageSize.toString())
//       .set("sortColumn", this.sortColumn)
//       .set("sortOrder", this.sortOrder)
//       .set("filterColumn", 'null')
//       .set("filterQuery", 'null');

//     this.teamsService.getTeams<PagedList<ITeamListViewModel>>(params).subscribe(result => {
//       this.teams = result.data
//     }, error => console.log(error));
//   }

//   onSubmit() {
//     var pilot = (this.id) ? this.pilot : <IPilotsListViewModel>{};

//     pilot.name = this.form.get('name').value;
//     pilot.nationality = this.form.get('nationality').value;
//     pilot.code = this.form.get('code').value;
//     pilot.number = this.form.get('number').value;
//     pilot.teamId = +this.form.get('teamId').value;

//     if (this.id) {
//       //update existing pilot
//       this.pilotService.putPilot(this.id, pilot).subscribe(result => {
//         this.router.navigate(['/pilots']);
//       }, error => console.log('error'));
//     }
//     else {
//       //create new pilot
//       this.pilotService.postPilot(pilot).subscribe(result => {
//         this.router.navigate(['/pilots']);
//       }, error => console.log(error));
//     }
//   }

//   CheckNameExists(): AsyncValidatorFn {
//     return (control: AbstractControl): Observable<{ [key: string]: any } | null> => {
//       var pilot = <IPilotDetailsViewModel>{};
//       pilot.id = (this.id) ? this.id : 0;
//       pilot.name = this.form.get('name').value;

//       return this.pilotService.checkNameExists<IPilotDetailsViewModel>(pilot).pipe(map(result => {
//         return (result ? { NameExists: true } : null)
//       }));
//     }
//   }
// }
