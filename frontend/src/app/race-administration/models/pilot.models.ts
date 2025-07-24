export interface IPilotsListViewModel {
  id: number;
  name: string;
  number: string;
  code: string;
  nationality: string;
  teamId: number;
}

export class PilotsListViewModel implements IPilotsListViewModel {
  id: number;
  name: string;
  number: string;
  code: string;
  nationality: string;
  teamId: number;

  constructor(init?: Partial<IPilotsListViewModel>) {
    this.id = init?.id || 0;
    this.name = init?.name || '';
    this.number = init?.number || '';
    this.code = init?.code || '';
    this.nationality = init?.nationality || '';
    this.teamId = init?.teamId || 0;
  }
}
