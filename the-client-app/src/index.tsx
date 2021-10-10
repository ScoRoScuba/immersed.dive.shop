import React from 'react';
import ReactDOM from 'react-dom';
import 'semantic-ui-css/semantic.min.css';
import './app/layout/styles.css';
import App from './app/layout/App';
import reportWebVitals from './reportWebVitals';
import { store, StoreContext } from './app/stores/store';
import { Router } from 'react-router-dom';
import {createBrowserHistory} from 'history';

export const history = createBrowserHistory();

ReactDOM.render(
    <React.StrictMode>
        <StoreContext.Provider value={store}>
            <Router history={history}>            
                <App />
            </Router>
        </StoreContext.Provider>
    </React.StrictMode>,
    document.getElementById('root')
);

reportWebVitals();

