
export interface Course {
    id?: string;    
    name?: string;
    description?: string;
    dateCreated?: Date;
    lastUpdated?: Date;
    live?:boolean;
}

export class Course implements Course {
    constructor(course?:Course) {
        if(course) {
            this.id = course.id;
            this.name = course.name;
            this.description = course.description;
            this.dateCreated = course.dateCreated;
            this.lastUpdated = course.lastUpdated;
            this.live = course.live;
        }
    }
}