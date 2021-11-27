import { observer } from "mobx-react-lite";
import React from "react";
import { Icon, Image, Menu, Table } from "semantic-ui-react";

export default observer( function Courses() {
  
    return (
        <>
        <Table singleLine selectable>
          <Table.Header>
            <Table.Row>
              <Table.HeaderCell>Course</Table.HeaderCell>
              <Table.HeaderCell>Next Date</Table.HeaderCell>
            </Table.Row>
          </Table.Header>
          <Table.Body>          
                <Table.Row>
                  <Table.Cell>Open Water</Table.Cell>
                  <Table.Cell>11th Nov 2021</Table.Cell>
            </Table.Row>          
                <Table.Row>
                  <Table.Cell>Rescue</Table.Cell>
                  <Table.Cell>11th Nov 2021</Table.Cell>
            </Table.Row>                      
            <Table.Row>
                  <Table.Cell>Advanced</Table.Cell>
                  <Table.Cell>11th Nov 2021</Table.Cell>
            </Table.Row>                      
        </Table.Body>

        <Table.Footer>
          <Table.Row>
            <Table.HeaderCell colSpan='2'>
              <Menu floated='right' pagination>
                <Menu.Item as='a' icon>
                  <Icon name='chevron left' />
                </Menu.Item>
                <Menu.Item as='a'>1</Menu.Item>
                <Menu.Item as='a'>2</Menu.Item>
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