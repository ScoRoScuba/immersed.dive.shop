import {makeAutoObservable} from "mobx";
import Course from "../models/course";

export default class CourseStore {

    coursesStore = new Map<string, Course>();

    constructor() {
        makeAutoObservable(this);

        // do we need a reaction to autoload ?
    }
}