import React, { Fragment, useEffect } from "react";
import { observer } from 'mobx-react-lite';
import { useStore } from "../../app/stores/store";
import { Header } from "semantic-ui-react";

export default observer( function CourseList() {
        const {courseStore} = useStore();
        const {allCourses} = courseStore;

        useEffect(()=>{
            courseStore.loadCourses();
        });    

        return(
            <Fragment>                
                {Array.from(allCourses).map( ([key,value]) => {                    
                    <Fragment key={key}>
                        <Header as='h4' content='Course List'/>
                        <Header as='h4' content={value.id}/>
                    </Fragment>                
                })}
            </Fragment>                
        );
})
