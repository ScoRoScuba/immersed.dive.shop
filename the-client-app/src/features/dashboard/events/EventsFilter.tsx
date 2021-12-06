import React, { Fragment } from "react";
import {  Dropdown, DropdownProps, Header,  Icon,  Menu } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";



export default function EventsFilter() {

    const {eventStore} = useStore();
    const {predicate, setPredicate} = eventStore;
   
    const weekOptions = [
        {
            key: 'all',
            text: 'All',
            value: 'All'
        },
        {
          key: 'thisWeek',
          text: 'This Week',
          value: 'This Week'
        },
        {
          key: 'nextWeek',
          text: 'Next Week',
          value: 'Next Week'
        },
        {
          key: 'thisMonth',
          text: 'This Month',
          value: 'This Month'
        },
        {
          key: 'comingMonth',
          text: 'Coming Month',
          value: 'Coming Month'
        },
        {
          key: 'nextMonth',
          text: 'Next Month',
          value: 'Next Month'
        }       
      ]
   
    function dateFilterOnChange(event: React.SyntheticEvent<HTMLElement>, data: DropdownProps)
    {
        var item = weekOptions.find( i => i.value === data.value!);
        setPredicate('calender', item!.key);
    }

    function couseFilterChange(event: React.SyntheticEvent<HTMLElement>, data: DropdownProps)
    {
      setPredicate('courseFilter', data!.key);
    }

    return (
        <Fragment>
            <Header as='h5' block>
                <Icon name='filter' />
                <Header.Content>Filters</Header.Content>
            </Header>
            <Dropdown
                floating
                labeled
                button
                icon='calendar'
                className='icon'
                style={{width: '100%'}}
                fluid
                selection     
                value={predicate.get('calendar')}           
                options={weekOptions}
                onChange={dateFilterOnChange}
              />
                            
            <Dropdown
                floating
                labeled
                button
                icon='drivers license'
                className='icon'
                placeholder='By Course'
                style={{width: '100%', marginTop:15}}
                onChange={couseFilterChange}
            >
                <Dropdown.Menu >
                    <Dropdown.Item text='All' />
                    <Dropdown.Item description='2' text='Open Water' />
                    <Dropdown.Item description='10' text='Advanced Open Water' />
                    <Dropdown.Item description='5' text='Rescue' />
                    <Dropdown.Item description='5' text='Divemaster' />
                </Dropdown.Menu>
            </Dropdown>

        </Fragment>
    );
}
