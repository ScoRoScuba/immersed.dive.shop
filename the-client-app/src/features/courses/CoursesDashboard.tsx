import React from "react";
import { observer } from 'mobx-react-lite';
import { Grid, Header } from 'semantic-ui-react';
import CourseList from './CourseList';

export default observer( function CoursesDashboard() {

        return(
            <>
                <Header as='h2'>
                    Courses Dashboard
                </Header>
                <Grid>
                    <Grid.Column width='10'>
                        load Courses here
                        <CourseList/>

                    </Grid.Column>
                    <Grid.Column width='6'>                
                        Filters
                    </Grid.Column>                    

                </Grid>
            </>
        );
})

