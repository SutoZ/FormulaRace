import { Component, OnInit, ViewChild } from '@angular/core';
import { TeamsService } from '../services/teams.service';
import { MatTableDataSource } from '@angular/material/table';
import { ITeamListViewModel as ITeamListViewModel } from '../models/team.models';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { HttpParams } from '@angular/common/http';

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

  defaultIndex = 0;
  defaultPageSize = 10;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private teamsSerivce: TeamsService) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    var event = new PageEvent();
    event.pageIndex = this.defaultIndex;
    event.pageSize = this.defaultPageSize;
    this.getTeams(event);
  }

  getTeams(event: PageEvent): void {
    var params = new HttpParams()
      .set('sortColumn', (this.sort) ? this.sort.active : this.defaultSortColumn)
      .set('sortOrder', (this.sort) ? this.sort.direction : this.defaultSortOder)
      .set('pageIndex', event.pageIndex.toString())
      .set('pageSize', event.pageSize.toString());

    this.teamsSerivce.getTeams(params).subscribe(result => {
      this.paginator.pageSize = result.pageSize;
      this.paginator.pageIndex = result.pageIndex;
      this.paginator.length = result.totalPages;
      this.dataSource = new MatTableDataSource<ITeamListViewModel>(result.data);
    }, error => console.log(error))
  }
}
