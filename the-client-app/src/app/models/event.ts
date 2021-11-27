import { Course } from "./course";
import { EventDate } from "./eventDate";
import { Participant } from "./participant";

export interface Event {
    id?: string;    
    dates?: EventDate[];
    courseId?: number;
    
    course?: Course;

    participants?: Participant[];

    startDate?: Date;

    dateCreated?: Date;
    lastUpdated?: Date;
    live?:boolean;        
}

export class Event implements Event {
    constructor(event?:Event) {
        if(event) {
            this.id = event.id;
            this.dates = event.dates;
            this.courseId = event.courseId;        
            this.participants = event.participants;
            this.startDate = event.startDate;
            this.dateCreated = event.dateCreated;
            this.lastUpdated = event.lastUpdated;
            this.live = event.live;
        }
    }
}