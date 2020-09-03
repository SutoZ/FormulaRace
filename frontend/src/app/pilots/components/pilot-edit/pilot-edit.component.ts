import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { IPilotsListViewModel } from '../../models/pilot.models';
import { ActivatedRoute, Router } from '@angular/router';
import { PilotsService } from '../../services/pilots.service';

@Component({
  selector: 'app-pilot-edit',
  templateUrl: './pilot-edit.component.html',
  styleUrls: ['./pilot-edit.component.css']
})
export class PilotEditComponent implements OnInit {

  title: string;
  form: FormGroup;
  pilot: IPilotsListViewModel;
  id?: number;

  constructor(
    private activatedRoute: ActivatedRoute,
    private pilotService: PilotsService,
    private router: Router) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl(''),
      number: new FormControl(''),
      code: new FormControl(''),
      nationality: new FormControl(''),
    });
    this.loadData();
  }

  loadData() {
    this.id = +this.activatedRoute.snapshot.paramMap.get('id');
    if (this.id) {
      this.pilotService.getPilotsById(this.id).subscribe(res => {
        this.pilot = res;
        this.title = 'Edit: ' + this.pilot.name;

        //update form with value of current pilot
        this.form.patchValue(this.pilot);
      },
        error => console.log(error));
    }
    else {
      this.title = 'Create new Pilot!';
    }
  }

  onSubmit() {
    var pilot = (this.id) ? this.pilot : <IPilotsListViewModel>{};

    pilot.name = this.form.get('name').value;
    pilot.nationality = this.form.get('nationality').value;
    pilot.code = this.form.get('code').value;
    pilot.number = this.form.get('number').value;
    //pilot.team = +this.form.get('team').value;

    if (this.id) {
      //update existing pilot
      this.pilotService.putPilot(this.id, pilot).subscribe(result => {
        this.router.navigate(['/pilots']);
      }, error => console.log('error'));
    }
    else {
      //create new pilot
      this.pilotService.postPilot(pilot).subscribe(result => {
        this.router.navigate(['/pilots']);
      }, error => console.log(error));
    }
  }
}
