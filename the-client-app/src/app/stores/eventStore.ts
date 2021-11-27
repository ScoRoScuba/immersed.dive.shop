import {makeAutoObservable, reaction} from "mobx";
import {Event} from "../models/event";
import agent from '../api/agent';

export default class EventStore {

    activeEvents = new Map<string, Event>();

    selectedEvent: Event | undefined = undefined;

    loadingEvents = false;    
    loadingInitial = false;

    predicate = new Map().set('week', true)


    constructor() {
        makeAutoObservable(this);
        // do we need a reaction to autoload ?

        reaction( 
            () => this.predicate.keys(),
            () => {
                this.activeEvents.clear();
                this.loadEvents();
            }
        )
    }

    get allEVents() {
        return this.activeEvents;
    }

    get eventsSortedByDate () {

        var array = Array.from(this.activeEvents.values());

        return array.sort((a,b)=> a.dateCreated!.getTime() - b.dateCreated!.getTime());        
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }   
    
    private setEvent( event : Event ){

        event.dateCreated = new Date(event.dateCreated!);
        event.lastUpdated = new Date(event.lastUpdated!);
        event.startDate = new Date(event.startDate!);

        this.activeEvents.set(event.id!, event)
    }

    loadEvents = async () => {
        this.loadingInitial = true;

        this.setLoadingInitial(true);

        try{
            const results = await agent.Events.list();

            results.forEach( event => {
                this.setEvent(event);    
            })

            this.setLoadingInitial(false);
        }catch(error){
            console.log(error);
            this.setLoadingInitial(false);
        }
    }

}