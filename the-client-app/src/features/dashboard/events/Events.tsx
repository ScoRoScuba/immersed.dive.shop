import { observer } from "mobx-react-lite";
import React from "react";
import { Image, Tab } from "semantic-ui-react";
import EventsList from  "./EventsList";

export default observer(function Events() {

    const panes = [
        {
            menuItem: 'This Week',
            render: () => <Tab.Pane attached='top'>
                            <EventsList/>
                        </Tab.Pane>,
        },
        {
            menuItem: 'Next Week',
            render: () => <Tab.Pane attached='top'>
                            <EventsList/>
                        </Tab.Pane>,
        },
        {
            menuItem: 'Month',
            render: () => <Tab.Pane attached='top'>
                            <EventsList/>
                        </Tab.Pane>
        },
    ]

    function onTabChange() {

    }
   
    return (
        <>
            <Tab menu={{ attached: 'bottom' }} panes={panes} onTabChange={onTabChange}/>
        </>
    );
})