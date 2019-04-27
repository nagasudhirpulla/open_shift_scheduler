import React from 'react';
import { render } from 'react-dom';
import './index.css';
// import Routes from './routes';
import { Provider } from 'react-redux';
// import { createBrowserHistory } from 'history'
import configureStore from './store/index';
import App from './components/App';
import ShiftsTable from './components/ShiftsTable';
// import registerServiceWorker from './registerServiceWorker';


// const history = createBrowserHistory()
const store = configureStore(history);

render(
    <Provider store={store}>
        <ShiftsTable />
    </Provider>,
    document.getElementById('root')
);
// registerServiceWorker();