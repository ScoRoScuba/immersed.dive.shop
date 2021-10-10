import React from 'react';
import { observer } from 'mobx-react-lite';
import { Container, Header, Segment, Button } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import { useStore } from '../../app/stores/store';

export default observer( function HomePage() {

    const {courseStore} = useStore();

    return (
        <Segment inverted textAlign='center' vertical className='masthead'>
            <Container text>
                <Header as='h1' inverted>
                    Dive School
                </Header>
                <>
                    <Header as='h2' inverted content='Welcome to the Dive School' />
                        <Button as={Link} to='/dashboard' size='huge' inverted>
                            Go to the Dive School;
                        </Button>                
                </>
            </Container>
        </Segment>
    )
});