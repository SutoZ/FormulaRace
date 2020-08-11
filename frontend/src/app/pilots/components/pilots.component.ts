import { Component, OnInit, Injectable } from '@angular/core';
import { PilotsService } from 'src/app/pilots/services/pilots.service';
import { Pilot } from '../pilot';

@Component({
  selector: 'app-pilots',
  templateUrl: './pilots.component.html',
  styleUrls: ['./pilots.component.css']
})

@Injectable({
  providedIn: 'root'
})

export class PilotsComponent implements OnInit {
  pilots: Pilot[];

  constructor(private pilotsServive: PilotsService) { }

  ngOnInit(): void {
    this.getPilots();
  }

  getPilots(): void {
    this.pilotsServive.getPilots().subscribe(pilots => this.pilots = pilots);
  }
}
