import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { HttpParams } from '@angular/common/http';
import { PagedList } from 'src/app/PagedList';
import { ITeamListViewModel } from '../../models/team.models';
import { TeamsService } from '../../services/teams.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css']
})
export class TeamsComponent implements OnInit {

  public dataSource = new MatTableDataSource<ITeamListViewModel>();
  public displayedColumns: string[] = ['Id', 'Name', 'DateOfFoundation', 'OwnerName', 'ChampionShipPoints'];
  public defaultSortColumn: string = "Name";
  public defaultSortOder: string = "asc";

  public defaultFilterColumn: string = "Name";
  filterQuery: string = "null";  

  defaultIndex = 0;
  defaultPageSize = 10;  

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) matSort: MatSort;

  constructor(private teamsSerivce: TeamsService) { }

  ngOnInit(): void {
    this.loadData(null);
  }

  loadData(query: string = null) {
    let event = new PageEvent();
    event.pageIndex = this.defaultIndex;
    event.pageSize = this.defaultPageSize;
    if (query) {
      this.filterQuery = query;
    }
    this.getTeams(event);
  }

  getTeams(event: PageEvent): void {
    let params = new HttpParams()      
    .set('pageIndex', event.pageIndex.toString())
    .set('pageSize', event.pageSize.toString())
    .set('sortColumn', (this.matSort) ? this.matSort.active : this.defaultSortColumn)
    .set('sortOrder', (this.matSort) ? this.matSort.direction : this.defaultSortOder)
    .set('filterColumn', this.defaultFilterColumn)
    .set('filterQuery', this.filterQuery);

    this.teamsSerivce.getTeams<PagedList<ITeamListViewModel>>(params).subscribe(result => {
      this.paginator.pageSize = result.pageSize;
      this.paginator.pageIndex = result.pageIndex;
      this.paginator.length = result.totalPages;
      this.dataSource = new MatTableDataSource<ITeamListViewModel>(result.data);
    }, error => console.log(error))
  }
}
