import { Component, OnInit, ViewChild } from '@angular/core';
import { PilotsService } from 'src/app/pilots/services/pilots.service';
import { HttpParams } from '@angular/common/http';
import { MatSort } from '@angular/material/sort';
import { PagedList } from 'src/app/PagedList';
import { IPilotsListViewModel } from '../../models/pilot.models';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-pilots',
  templateUrl: './pilots.component.html',
  styleUrls: ['./pilots.component.css'],
  standalone: false
})

export class PilotsComponent implements OnInit {
  public displayedColumns: string[] = ['Id', 'Name', 'Number', 'Code', 'Nationality'];
  public dataSource = new MatTableDataSource<IPilotsListViewModel>();

  defaultIndex = 0;
  defaultPageSize = 10;

  public defaultSortColumn: string = "Name";
  public defaultSortOrder: string = "asc";
  public defaultFilterColumn = "Name";

  filterQuery: string = '';

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private readonly pilotsService: PilotsService) { }

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.loadData();
  }

  ngafterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  loadData(query: string = '') {
    let event = new PageEvent();
    event.pageIndex = this.defaultIndex;
    event.pageSize = this.defaultPageSize;

    if (query) {
      this.filterQuery = query;
    }
    this.getPilots(event);
  }

  getPilots(event: PageEvent) {
    let params = new HttpParams()
      .set("pageIndex", event.pageIndex.toString())
      .set("pageSize", event.pageSize.toString())
      .set("sortColumn", (this.sort) ? this.sort.active : this.defaultSortColumn)
      .set("sortOrder", (this.sort) ? this.sort.direction : this.defaultSortOrder)
      .set("filterColumn", this.defaultFilterColumn)
      .set("filterQuery", this.filterQuery);

    this.pilotsService.getPilots<PagedList<IPilotsListViewModel>>(params).subscribe({
      next: result => {
        this.paginator.length = result.totalCount;
        this.paginator.pageIndex = result.pageIndex;
        this.paginator.pageSize = result.pageSize;
        this.dataSource.data = result.data;
        this.dataSource.paginator = this.paginator;
      },
      error: error => console.error(error)
    });
  }
}
