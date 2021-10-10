
export interface Course {
    id?: string;    
}

export class Course implements Course {
    constructor(course?:Course) {
        if(course) {
            this.id = course.id;
        }
    }
}