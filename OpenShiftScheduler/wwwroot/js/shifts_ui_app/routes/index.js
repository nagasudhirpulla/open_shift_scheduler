/*
https://github.com/SophieDeBenedetto/catbook-redux/blob/blog-post/src/routes.js
*/
import React from 'react';
import { Route, Switch } from 'react-router-dom';
import App from '../components/App';
import { ConnectedRouter } from 'connected-react-router'
/*
  <BrowserRouter>
    <Route path='/' component={Dashboard} />
  </BrowserRouter>
*/
export default (props) => (
  <ConnectedRouter history={props.history}>
    <Switch>
      <Route exact path='/' component={App} />      
    </Switch>
  </ConnectedRouter>
);