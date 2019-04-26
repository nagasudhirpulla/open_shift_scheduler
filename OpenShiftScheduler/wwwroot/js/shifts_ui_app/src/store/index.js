/*
Using connectedRouter for changing routes in redux actions
https://github.com/supasate/connected-react-router
*/
import { createStore, applyMiddleware } from 'redux';
import rootReducer from '../reducers'
import thunk from 'redux-thunk';
// import { connectRouter, routerMiddleware } from 'connected-react-router'

export default function configureStore() {
  return createStore(
    rootReducer,
    applyMiddleware(thunk)
  );
}