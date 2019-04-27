/*
https://github.com/SophieDeBenedetto/catbook-redux/blob/blog-post/src/reducers/index.js

Using nested reducers in redux
https://stackoverflow.com/questions/36786244/nested-redux-reducers
*/
import {combineReducers} from 'redux';
import shifts_ui from './shifts_ui';

const rootReducer = combineReducers({
  // list of reducers here comma separated
  shifts_ui
});

export default rootReducer;