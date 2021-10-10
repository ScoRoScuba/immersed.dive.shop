import React from "react";
import { observer } from 'mobx-react-lite';
import { Container, Header, Segment, Image, Button, Divider } from 'semantic-ui-react';

export default observer( function CoursesDashboard() {

        return(
            <>
                <Header as='h2' inverted>
                    Courses Dashboard
                </Header>
            </>
        );
})

