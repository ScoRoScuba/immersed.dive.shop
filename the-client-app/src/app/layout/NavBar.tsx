import { observer } from "mobx-react-lite";
import React from "react";
import { NavLink } from "react-router-dom";
import { Container, Menu} from "semantic-ui-react";

export default observer( function NavBar(){

    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item as={NavLink} to='/' exact header>
                    <img src="/assets/logo.png" alt="logo" style={{marginRight: '10px'}}/>
                </Menu.Item>
                <Menu.Item as={NavLink} to='/dashboard' name='Dashboard'/>
                <Menu.Item as={NavLink} to='/courses' name='Courses'/>

                {/* <Menu.Item>
                    <Button as={NavLink} to='/createActivity' positive content='Create Activity'/>                        
                </Menu.Item>
                <Menu.Item position='right'>
                    <Image  src={user?.image || '/assets/user.png'} avatar spaced='right'/>
                    <Dropdown pointing='top left' text={user?.displayName}>
                        <Dropdown.Menu>
                            <Dropdown.Item as={Link} to={`/profiles/${user?.userName}`} text='My Profile' icon='user'/>
                            <Dropdown.Item onClick={logout} text='Logout' icon='power' />
                        </Dropdown.Menu>
                    </Dropdown>
                </Menu.Item> */}

            </Container>                
        </Menu>
    )
})