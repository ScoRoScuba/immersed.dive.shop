export interface EventDate {
    id?: string;    
    date?: Date;
    estimatedDuration?: number;
    dateCreated?: Date;
    lastUpdated?: Date;
    live?:boolean;    
}

export class EventDate implements  EventDate {
    constructor(eventDate?: EventDate){
        if(eventDate) {
            this.id = eventDate.id;
            this.date = eventDate.date;
            this.estimatedDuration = eventDate.estimatedDuration;            
            this.dateCreated = eventDate.dateCreated;
            this.lastUpdated = eventDate.lastUpdated;
            this.live =eventDate.live;
        }
    }
}
