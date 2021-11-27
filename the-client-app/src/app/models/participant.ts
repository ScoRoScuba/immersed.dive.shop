
export interface Participant {
    id?: string;    
    eventId?: string;
    personId?:string;
    dateRegistered?: Date
    dateConfirmed?:Date
}

export class Participant implements Participant {
    constructor(participant?:Participant) {
        if(participant) {
            this.id = participant.id;
            this.eventId = participant.eventId;
            this.personId = participant.personId;
            this.dateRegistered = participant.dateRegistered;
            this.dateConfirmed = participant.dateConfirmed;
        }
    }
}
