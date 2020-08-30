import { IPilotsListViewModel } from 'src/app/pilots/models/pilot.models';

export class ITeamListViewModel {
    id: string;
    name: string | null;
    dateOfFoundation: string | null;
    ownerName: string | null;
    championShipPoints: string | null;
    pilots?: IPilotsListViewModel | null;
}
