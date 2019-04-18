import './index.css';
import Routes from './routes';
import { Provider } from 'react-redux';
import { createBrowserHistory } from 'history'
import configureStore from './store/index';


const history = createBrowserHistory()
const store = configureStore(history);

ReactDom.render(
    <Provider store={store}>
        <Routes history={history} />
    </Provider>,
    document.getElementById('content')
);
registerServiceWorker();