import { Component, OnInit, ViewChild } from '@angular/core';
import { PilotsService } from 'src/app/pilots/services/pilots.service';
import { HttpParams } from '@angular/common/http';
import { MatSort, SortDirection } from '@angular/material/sort';
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

export class PilotsComponent {
  public displayedColumns: string[] = ['Id', 'Name', 'Number', 'Code', 'Nationality'];
  public dataSource = new MatTableDataSource<IPilotsListViewModel>();

  defaultIndex = 0;
  defaultPageSize = 10;

  defaultSortColumn: string = "Name";
  defaultSortOrder: SortDirection = 'asc'; // or 'desc' or ''
  defaultFilterColumn = "Name";

  filterQuery: string = '';

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private readonly pilotsService: PilotsService) { }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.loadData();
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
        // Check if paginator is defined before setting properties
        if (this.paginator) {
          // Ensure paginator is defined before setting properties
          this.paginator.length = result.totalCount;
          this.paginator.pageIndex = result.pageIndex;
          this.paginator.pageSize = result.pageSize;
        }
        console.log('API result:', result);
        
        this.dataSource.data = result.data;
        this.dataSource.paginator = this.paginator;
      },
      error: error => console.error(error)
    });
  }
}
