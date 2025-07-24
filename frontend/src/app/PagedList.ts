export interface IPagedList<T> {
  data: T[];
  pageIndex: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasPreviousPage: boolean;
  sortColumn: string;
  sortOrder: string;
  filterColumn: string;
  filterQuery: string;
}

export class PagedList<T> implements IPagedList<T> {
  data: T[] = [];
  pageIndex = 0;
  pageSize = 10;
  totalCount = 0;
  totalPages = 0;
  hasPreviousPage = false;
  sortColumn = '';
  sortOrder = '';
  filterColumn = '';
  filterQuery = '';

  constructor(init?: Partial<PagedList<T>>) {
    Object.assign(this, init);
  }
}
