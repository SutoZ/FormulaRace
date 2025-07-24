export interface ITeamListViewModel {
  id: string;
  name: string;
  dateOfFoundation: string;
  ownerName: string;
  championShipPoints: string;
}

export class TeamListViewModel implements ITeamListViewModel {
  id: string;
  name: string;
  dateOfFoundation: string;
  ownerName: string;
  championShipPoints: string;

  constructor(init?: Partial<ITeamListViewModel>) {
    this.id = init?.id || '';
    this.name = init?.name || '';
    this.dateOfFoundation = init?.dateOfFoundation || '';
    this.ownerName = init?.ownerName || '';
    this.championShipPoints = init?.championShipPoints || '';
  }
}
