import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { Button, Icon, Menu,  Table } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { format } from "date-fns";

export default observer( function EventsList() {

    const {eventStore} = useStore();
    const {eventsSortedByDate, activeEvents, loadEvents } = eventStore;

    useEffect(()=> {    
      if(activeEvents.size <= 1 ) loadEvents();
    },[activeEvents.size,loadEvents])
  
    return (
        <>
        <Table singleLine selectable>
          <Table.Header>
          <Table.Row>
            <Table.HeaderCell colSpan='5'>
            <Button
                floated='left'
                icon
                labelPosition='left'
                primary
                size='small'
              >
                <Icon name='add' /> Add Event
              </Button>
              <Menu floated='right' pagination>
                <Menu.Item as='a' icon>
                  <Icon name='chevron left' />
                </Menu.Item>
                <Menu.Item as='a'>1</Menu.Item>
                <Menu.Item as='a'>2</Menu.Item>
                <Menu.Item as='a'>3</Menu.Item>
                <Menu.Item as='a'>4</Menu.Item>
                <Menu.Item as='a' icon>
                  <Icon name='chevron right' />
                </Menu.Item>
              </Menu>
            </Table.HeaderCell>
          </Table.Row>

            <Table.Row>
              <Table.HeaderCell>Course</Table.HeaderCell>
              <Table.HeaderCell>Dates</Table.HeaderCell>
              <Table.HeaderCell>Duration</Table.HeaderCell>
              <Table.HeaderCell>Max. Number</Table.HeaderCell>
              <Table.HeaderCell>Booked</Table.HeaderCell>
            </Table.Row>
          </Table.Header>
          <Table.Body>          
          {eventsSortedByDate.map( item => (
                <Table.Row key={item.id}>
                  <Table.Cell>{item.course?.name}</Table.Cell>
                  <Table.Cell>{format(item.startDate!, 'dd MMM yyyy')}</Table.Cell>
                  <Table.Cell>{
                      ( item.dates !== undefined && item.dates.length > 0 ? 
                        item.dates.reduce((sum, current) => sum + current.estimatedDuration!, 0) : 1)
                    }</Table.Cell>
                  <Table.Cell>{5}</Table.Cell>
                  <Table.Cell>{
                      ( item.participants !== undefined && item.participants.length > 0 ? 
                        item.participants.length : 0)                    
                    }</Table.Cell>
                </Table.Row>
              ))}
        </Table.Body>

        <Table.Footer>
          <Table.Row>
            <Table.HeaderCell colSpan='5'>
            <Button
                floated='left'
                icon
                labelPosition='left'
                primary
                size='small'
              >
                <Icon name='add' /> Add Event
              </Button>
              <Menu floated='right' pagination>
                <Menu.Item as='a' icon>
                  <Icon name='chevron left' />
                </Menu.Item>
                <Menu.Item as='a'>1</Menu.Item>
                <Menu.Item as='a'>2</Menu.Item>
                <Menu.Item as='a'>3</Menu.Item>
                <Menu.Item as='a'>4</Menu.Item>
                <Menu.Item as='a' icon>
                  <Icon name='chevron right' />
                </Menu.Item>
              </Menu>
            </Table.HeaderCell>
          </Table.Row>
        </Table.Footer>    
      </Table>  
        </>
    );
})