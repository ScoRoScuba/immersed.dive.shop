import React from "react";
import { Grid, Header, Image } from "semantic-ui-react";
import Courses from "./courses/Courses";
import { observer } from "mobx-react-lite";
import EventsList from "./events/EventsList";
import EventsFilter from "./events/EventsFilter";

export default observer( function Dashboard() {

    return (
        <>
            <Header as='h2'>
                Courses
            </Header>

            <Grid>
                <Grid.Column width="12">
                    <EventsList />
                </Grid.Column>
                <Grid.Column width="4">
                    <EventsFilter/>
                </Grid.Column>
            </Grid>
        </>
    );
})
