import React, { Fragment, useEffect } from 'react'
import './styles.css';
import NavBar from './NavBar';
import {Container, Header} from 'semantic-ui-react';
import { Route, Switch } from 'react-router';
import HomePage from '../../features/home/HomePage';
import CoursesDashboard from '../../features/courses/CoursesDashboard';
import Dashboard from '../../features/dashboard/Dashboard';
import agent from '../api/agent';
import { userInfo } from 'os';
import { useStore } from '../stores/store';

export default function App() {
        return(
            <Fragment>
                <Header as='h2' content='Immersed'/>

                <Route exact path='/' component={HomePage}/>
                <Route 
                    path={'/(.+)'}
                    render={()=>(
                        <Fragment>
                        <NavBar/>
                        <Container style = {{marginTop: '7em'}}>
                            <Switch>
                            <Route exact path='/dashboard' component={Dashboard}/>              
                            <Route exact path='/courses' component={CoursesDashboard}/>
                            {/* <Route path='/activities/:id' component={ActivityDetails}/>
                            <Route key={location.key} path={['/createActivity', '/manage/:id']} component={ActivityForm}/>
                            <Route path='/profiles/:username' component={ProfilePage}/>                  
                            <Route path='/errors' component={TestErrors}/>
                            <Route path='/server-error' component={ServerError}/>
                            <Route component={NotFound}/> */}
                            </Switch>          
                        </Container>            
                        </Fragment>
                    )}
                />
            </Fragment>
    
        );
}


