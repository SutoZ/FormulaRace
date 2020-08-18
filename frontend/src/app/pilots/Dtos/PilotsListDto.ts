export interface IPilotsListDto {
    pilotId: number,
    name?: string | undefined,
    number?: number | undefined,
    code?: string | undefined,
    nationality?: string | undefined
}

export class PilotsListDto implements IPilotsListDto {
    pilotId: number;
    name?: string;
    number?: number;
    code?: string;
    nationality?: string;


    constructor(data?: IPilotsListDto) {
        if (data) {
            this.pilotId = data.pilotId,
                this.number = data.number,
                this.nationality = data.nationality,
                this.name = data.name,
                this.code = data.code
        }
    }
}