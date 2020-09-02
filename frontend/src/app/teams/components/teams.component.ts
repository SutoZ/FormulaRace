import { Component, OnInit, ViewChild } from '@angular/core';
import { TeamsService } from '../services/teams.service';
import { MatTableDataSource } from '@angular/material/table';
import { ITeamListViewModel as ITeamListViewModel } from '../models/team.models';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { HttpParams } from '@angular/common/http';
import { PagedList } from 'src/app/PagedList';

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
    var event = new PageEvent();
    event.pageIndex = this.defaultIndex;
    event.pageSize = this.defaultPageSize;
    if (query) {
      this.filterQuery = query;
    }
    this.getTeams(event);
  }

  getTeams(event: PageEvent): void {
    var params = new HttpParams()      
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
