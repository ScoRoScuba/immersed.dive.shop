import {makeAutoObservable, reaction} from "mobx";
import {Course} from "../models/course";
import agent from '../api/agent';

export default class CourseStore {

    courseStore = new Map<string, Course>();

    loadingCourses = false;    
    loadingInitial = false;

    constructor() {
        makeAutoObservable(this);

        // do we need a reaction to autoload ?
    }

    get allCourses() {
        return this.courseStore;
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }       

    loadCourses = async () => {
        this.loadingInitial = true;
        this.setLoadingInitial(true);
        try{
            const results = await agent.Courses.list();
            results.forEach( course => {
                this.courseStore.set(course.id!, course)
            })
            this.setLoadingInitial(false);
        }catch(error){
            console.log(error);
            this.setLoadingInitial(false);
        }
    }
}