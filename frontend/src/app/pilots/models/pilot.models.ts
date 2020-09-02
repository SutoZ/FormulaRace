import { ITeamListViewModel } from 'src/app/teams/models/team.models';

export interface IPilotsListViewModel {
    id: number,
    name?: string | undefined,
    number?: string | undefined,
    code?: string | undefined,
    nationality?: string | undefined,
    team?: ITeamListViewModel | undefined
}