export interface PagedList<T> {
    data: T[],
    pageIndex: number,
    pageSize: number,
    totalCount: number,
    totalPages: number,
    hasPreviousPage: boolean,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string,
    filterQuery: string
}