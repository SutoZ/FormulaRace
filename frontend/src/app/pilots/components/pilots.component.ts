import { Component, OnInit, Injectable, ViewChild } from '@angular/core';
import { PilotsService } from 'src/app/pilots/services/pilots.service';
import { Pilot } from '../pilot';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-pilots',
  templateUrl: './pilots.component.html',
  styleUrls: ['./pilots.component.css']
})

@Injectable({
  providedIn: 'root'
})

export class PilotsComponent implements OnInit {
 public pilots: Pilot[];

  public displayedColumns: String[] = ['Id', 'Name', 'Number', 'Code', 'Nationality'];
  public dataSource = this.pilots;

  constructor(private pilotsServive: PilotsService) { }

  ngOnInit(): void {
    this.getPilots();
  }

  getPilots(): void {
    this.pilotsServive.getPilots().subscribe(result => {this.pilots = result; }, error => console.error(error));
  }
}
