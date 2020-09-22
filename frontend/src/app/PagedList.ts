export interface PagedList<T> {
    data: T[],
    pageIndex: number,
    pageSize: number,
    count: number,
    totalPages: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string,
    filterQuery: string
}