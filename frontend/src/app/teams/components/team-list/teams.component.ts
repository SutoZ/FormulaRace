// import { Component, OnInit, ViewChild } from '@angular/core';
// import { MatSort, SortDirection } from '@angular/material/sort';
// import { HttpParams } from '@angular/common/http';
// import { PagedList } from 'src/app/PagedList';
// import { ITeamListViewModel } from '../../models/team.models';
// import { TeamsService } from '../../services/teams.service';
// import { MatPaginator, PageEvent } from '@angular/material/paginator';
// import { MatTableDataSource } from '@angular/material/table';

// @Component({
//   selector: 'app-teams',
//   templateUrl: './teams.component.html',
//   styleUrls: ['./teams.component.css'],
//   standalone: false
// })
// export class TeamsComponent implements OnInit {

//   public dataSource = new MatTableDataSource<ITeamListViewModel>();
//   public displayedColumns: string[] = ['Id', 'Name', 'DateOfFoundation', 'OwnerName', 'ChampionShipPoints'];
//   public defaultSortColumn: string = "Name";
//   defaultSortOrder: SortDirection = 'asc'; // or 'desc' or ''

//   public defaultFilterColumn: string = "Name";
//   filterQuery: string = "null";

//   defaultIndex = 0;
//   defaultPageSize = 10;

//   @ViewChild(MatPaginator) paginator!: MatPaginator;
//   @ViewChild(MatSort) matSort!: MatSort;

//   constructor(private readonly teamsSerivce: TeamsService) { }

//   ngOnInit() {
//     this.loadData();
//   }

//   loadData(query: string = '') {
//     let event = new PageEvent();
//     event.pageIndex = this.defaultIndex;
//     event.pageSize = this.defaultPageSize;
//     if (query) {
//       this.filterQuery = query;
//     }
//     this.getTeams(event);
//   }

//   getTeams(event: PageEvent) {
//     let params = new HttpParams()
//       .set('pageIndex', event.pageIndex.toString())
//       .set('pageSize', event.pageSize.toString())
//       .set('sortColumn', (this.matSort) ? this.matSort.active : this.defaultSortColumn)
//       .set('sortOrder', (this.matSort) ? this.matSort.direction : this.defaultSortOrder)
//       .set('filterColumn', this.defaultFilterColumn)
//       .set('filterQuery', this.filterQuery);

//     this.teamsSerivce.getTeams<PagedList<ITeamListViewModel>>(params).subscribe({
//       next: result => {
//         this.paginator.pageSize = result.pageSize;
//         this.paginator.pageIndex = result.pageIndex;
//         this.paginator.length = result.totalPages;
//         this.dataSource = new MatTableDataSource<ITeamListViewModel>(result.data);
//       },
//       error: error => console.log(error)
//     });
//   }
// }
