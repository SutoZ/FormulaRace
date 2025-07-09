import { AfterViewInit, Component, OnDestroy, ViewChild } from '@angular/core';
import { PilotsService } from 'src/app/pilots/services/pilots.service';
import { HttpParams } from '@angular/common/http';
import { MatSort, MatSortModule, SortDirection } from '@angular/material/sort';
import { PagedList } from 'src/app/PagedList';
import { IPilotsListViewModel } from '../../models/pilot.models';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { PilotDeleteComponent } from '../pilot-delete-component/pilot-delete-component';
import { MatDialog } from '@angular/material/dialog';
import { RouterModule } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Subscription } from 'rxjs/internal/Subscription';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { catchError, map, merge, startWith, Subject, switchMap } from 'rxjs';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-pilot-list-component',
  templateUrl: './pilot-list-component.html',
  styleUrls: ['./pilot-list-component.css'],
  standalone: true,
  imports: [
    MatTableModule,
    MatPaginatorModule,
    MatInputModule,
    MatSortModule,
    RouterModule,
    MatFormFieldModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatIconModule,
    CommonModule
  ]
})

export class PilotListComponent implements AfterViewInit, OnDestroy {
  public displayedColumns: string[] = ['Id', 'Name', 'Number', 'Code', 'Nationality', 'actions'];
  public dataSource = new MatTableDataSource<IPilotsListViewModel>();

  pageIndex = 0;
  pageSize = 10;
  length = 0;
  isLoading = true;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  defaultSortColumn: string = 'Name';
  defaultSortOrder: SortDirection = 'asc'; // or 'desc' or ''
  defaultFilterColumn = 'Name';

  private filterSubject = new Subject<string>();
  private componentDestroyed$ = new Subject<void>();
  private dataSub?: Subscription;

  filterQuery: string = '';


  constructor(private readonly dialog: MatDialog, private readonly pilotsService: PilotsService) { }


  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;

    this.dataSub = merge(this.sort.sortChange, this.paginator.page, this.filterSubject.pipe(startWith(''))).pipe(
      switchMap(() => {
        this.isLoading = true;
        const params = new HttpParams()
          .set("pageIndex", this.paginator.pageIndex.toString())
          .set("pageSize", this.paginator.pageSize.toString())
          .set("sortColumn", this.sort.active || this.defaultSortColumn)
          .set("sortOrder", this.sort.direction || this.defaultSortOrder)
          .set("filterColumn", this.defaultFilterColumn)
          .set("filterQuery", this.filterQuery);

        return this.pilotsService.getPilots<PagedList<IPilotsListViewModel>>(params);
      }),
      map(result => {
        this.isLoading = false;
        this.length = result.totalCount;
        return result.data;
      }),
      catchError(() => {
        this.isLoading = false;
        // Handle error and return an empty array, show a snackbar, or log the error
        console.error('Error loading pilots data');
        return [];
      })
    ).subscribe(data => {
      this.dataSource.data = data;
    });
  }

  // Called from the template's filter input
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.filterQuery = filterValue.trim().toLowerCase();
    // Reset paginator to the first page when filtering
    this.paginator.pageIndex = 0;
    this.filterSubject.next(this.filterQuery);
  }

  openDeleteDialog(pilotId: number) {
    const dialogRef = this.dialog.open(PilotDeleteComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result === 'yes') {
        this.pilotsService.deletePilot(pilotId).subscribe({
          next: () => {
            console.log(`Pilot with ID ${pilotId} deleted successfully.`);
            this.filterSubject.next(this.filterQuery); // Reload data after deletion
          },
          error: error => console.error(error)
        });
      }
    });
  }

  ngOnDestroy() {
    this.componentDestroyed$.next();
    this.componentDestroyed$.complete();

    if (this.dataSub) {
      this.dataSub.unsubscribe();
    }
  }
}