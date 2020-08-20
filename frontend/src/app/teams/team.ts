import { IPilotViweModel } from 'src/app/pilots/pilot';

export class ITeamViewModel {
    Id: number;
    Name: string;
    DateOfFoundation: string;
    OwnerName: string;
    ChampionShipPoints: number;
    Pilots: IPilotViweModel[];
}