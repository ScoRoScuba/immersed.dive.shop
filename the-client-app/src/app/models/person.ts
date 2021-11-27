
export interface Person {
    id?: string;    
    fullName?: string;    
    familyName?: string;    
    name?: string;    
    otherNames?: string;    
    knownAs?: string;    
    dateOfBirth?: Date
    sex?: string;    
    identifiesAs?: string;    
    dateCreated?: Date;
    lastUpdated?: Date;
    live?:boolean;      
}

export class Person implements Person {
    constructor(person?:Person) {
        if(person) {
            this.id = person.id;

            this.fullName = person.fullName;
            this.familyName = person.familyName;
            this.name = person.name;
            this.otherNames = person.otherNames;
            this.knownAs = person.knownAs;
            this.dateOfBirth = person.dateOfBirth;
            this.sex = person.sex;
            this.identifiesAs = person.identifiesAs;
            
            this.dateCreated = person.dateCreated;
            this.lastUpdated = person.lastUpdated;
            this.live = person.live;
        }
    }
}
