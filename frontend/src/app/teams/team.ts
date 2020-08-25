import { IPilotViweModel } from '../pilots/IPilotViewModel';

export class ITeamViewModel {
    Id: number;
    Name: string;
    DateOfFoundation: string;
    OwnerName: string;
    ChampionShipPoints: number;
    Pilots: IPilotViweModel[];
}