import { Component, OnInit, Injectable, ViewChild } from '@angular/core';
import { PilotsService } from 'src/app/pilots/services/pilots.service';
import { IPilotViweModel as IPilotViewModel } from '../IPilotViewModel';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { HttpParams, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-pilots',
  templateUrl: './pilots.component.html',
  styleUrls: ['./pilots.component.css']
})

@Injectable({
  providedIn: 'root'
})

export class PilotsComponent implements OnInit {
  public pilots: MatTableDataSource<IPilotViewModel>;

  public displayedColumns: String[] = ['Id', 'Name', 'Number', 'Code', 'Nationality'];
  public dataSource = this.pilots;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private pilotsService: PilotsService, private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    var pageEvent = new PageEvent();
    pageEvent.pageIndex = 0;
    pageEvent.pageSize = 5;
    this.getPilots(pageEvent);
  }

  getPilots(event: PageEvent): void {
    var params = new HttpParams()
    .set("pageIndex", event.pageIndex.toString())
    .set("pageSize", event.pageSize.toString());

    this.pilotsService.getPilots(event, params).subscribe(result => {
      this.paginator.length = result.totalPages;
      this.paginator.pageIndex = result.pageIndex;
      this.paginator.pageSize = result.pageSize;
      this.pilots = new MatTableDataSource<IPilotViewModel>(result.data);

    }, error => console.error(error));
  }
}
