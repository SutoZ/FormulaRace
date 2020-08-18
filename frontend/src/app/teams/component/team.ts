import { Pilot } from 'src/app/pilots/pilot';

export class Team {
    Id: number;
    Name: string;
    DateOfFoundation: string;
    OwnerName: string;
    ChampionShipPoints: number;
    Pilots: Pilot[];
}