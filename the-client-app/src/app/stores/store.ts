import { createContext, useContext } from 'react';
import CourseStore from "./coursesStore";

interface Store {
    courseStore: CourseStore;    
}

export const store: Store = {
    courseStore: new CourseStore(),    
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}