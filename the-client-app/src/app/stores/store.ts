import { createContext, useContext } from 'react';
import CourseStore from "./coursesStore";
import EventStore from "./eventStore";

interface Store {
    courseStore: CourseStore;
    eventStore: EventStore;    
}

export const store: Store = {
    courseStore: new CourseStore(),    
    eventStore: new EventStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}