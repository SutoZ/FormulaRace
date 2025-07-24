import { HttpParams } from '@angular/common/http';
import { AfterViewInit, Component, OnDestroy, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatFormField } from '@angular/material/form-field';
import { MatIcon } from '@angular/material/icon';
import { MatPaginator } from '@angular/material/paginator';
import { MatProgressSpinner } from '@angular/material/progress-spinner';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort, SortDirection } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { RouterModule } from '@angular/router';
import {
  catchError,
  map,
  merge,
  startWith,
  Subject,
  Subscription,
  switchMap,
  takeUntil,
} from 'rxjs';
import { IPilotsListViewModel } from 'src/app/race-administration/models/pilot.models';
import { PilotDeleteComponent } from '../pilot-delete-component/pilot-delete-component';
import { CommonModule } from '@angular/common';
import { PilotsService } from '../../services/pilotservice';

@Component({
  selector: 'app-pilot-list-component',
  imports: [
    RouterModule,
    MatIcon,
    CommonModule,
    MatPaginator,
    MatFormField,
    MatTableModule,
    MatProgressSpinner,
    MatSort,
    MatFormField,
  ],
  templateUrl: './pilot-list-component.html',
  styleUrl: './pilot-list-component.css',
})
export class PilotListComponent implements AfterViewInit, OnDestroy {
  displayedColumns: string[] = ['Name', 'Number', 'Code', 'Nationality', 'actions'];
  dataSource = new MatTableDataSource<IPilotsListViewModel>([]);
  pageIndex = 0;
  pageSize = 10;
  isLoading = true;
  length = 0;
  filterQuery = '';

  defaultSortColumn: string = 'Name';
  defaultSortOrder: SortDirection = 'asc'; // or 'desc' or ''
  defaultFilterColumn = 'Name';
  defaultFilterQuery = '';

  private dataSub?: Subscription;
  private readonly filter$ = new Subject<string>();
  private readonly componentDestroyed$ = new Subject<void>();

  constructor(
    private readonly pilotsService: PilotsService,
    private readonly snackBar: MatSnackBar,
    private readonly dialog: MatDialog
  ) {}

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;

    this.dataSub = merge(
      this.sort.sortChange,
      this.paginator.page,
      this.filter$.pipe(startWith(''))
    )
      .pipe(
        switchMap(() => {
          this.isLoading = true;
          const params = new HttpParams()
            .set('pageIndex', this.paginator.pageIndex.toString())
            .set('pageSize', this.paginator.pageSize.toString())
            .set('sortColumn', this.sort.active || this.defaultSortColumn)
            .set('sortOrder', this.sort.direction || this.defaultSortOrder)
            .set('filterColumn', this.defaultFilterColumn)
            .set('filterQuery', this.defaultFilterQuery);

          return this.pilotsService.getPilots(params);
        }),
        map((result) => {
          this.isLoading = false;
          this.length = result.totalCount;
          return result.data;
        }),
        catchError(() => {
          this.isLoading = false;
          // Handle error and return an empty array, show a snackbar, or log the error
          this.snackBar.open('Error loading pilots data', 'Close', {
            duration: 3000,
          });
          // Return an empty array to avoid breaking the table
          return [];
        }),
        takeUntil(this.componentDestroyed$)
      )
      .subscribe((data) => {
        this.dataSource.data = data;
      });
  }

  openDeleteDialog(pilotId: number) {
    const dialogRef = this.dialog.open(PilotDeleteComponent);

    dialogRef.afterClosed().subscribe((result) => {
      if (result === 'yes') {
        this.pilotsService.deletePilot(pilotId).subscribe({
          next: () => {
            console.log(`Pilot with ID ${pilotId} deleted successfully.`);
            this.filter$.next(this.filterQuery); // Reload data after deletion
          },
          error: (error) => console.error(error),
        });
      }
    });
  }

  trackPilotId(index: number, pilot: IPilotsListViewModel): number {
    return pilot.id;
  }

  // Called from the template's filter input
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.filterQuery = filterValue.trim().toLowerCase();
    // Reset paginator to the first page when filtering
    this.paginator.pageIndex = 0;
    this.filter$.next(this.filterQuery);
  }

  ngOnDestroy(): void {
    this.dataSub?.unsubscribe();

    this.filter$.next('');
    this.filter$.complete();

    this.componentDestroyed$.next();
    this.componentDestroyed$.complete();

    this.dataSource.disconnect();
  }
}
