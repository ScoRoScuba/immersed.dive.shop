import React from "react";
import { Grid, Header, Image } from "semantic-ui-react";
import Events from "./events/Events";
import Courses from "./courses/Courses";
import { observer } from "mobx-react-lite";

export default observer( function Dashboard() {

    return (
        <>
            <Header as='h2'>
                Dashboard
            </Header>

            <Grid>
                <Grid.Column width="12">
                    <Events />
                </Grid.Column>
                <Grid.Column width="4">
                    filters
                </Grid.Column>
            </Grid>
            <Grid>        
                <Grid.Column width="6">
                    <Courses/>
                </Grid.Column>
                <Grid.Column width="6">
                    <Image src='https://react.semantic-ui.com/images/wireframe/paragraph.png' />
                </Grid.Column>        
            </Grid>


        </>
    );
})
