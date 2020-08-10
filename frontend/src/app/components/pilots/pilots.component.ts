import { Component, OnInit, Inject, Injectable, Optional } from '@angular/core';
import { Pilot } from './pilot';
import { HttpClient } from '@angular/common/http';
import { PilotsService } from 'src/app/pilots.service';

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
